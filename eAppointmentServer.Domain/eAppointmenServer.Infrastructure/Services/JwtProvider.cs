using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using eAppointmentServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eAppointmentServer.Infrastructure.Services;

        internal sealed class JwtProvider(
        IConfiguration configuration,
        IUserRoleRepository userRoleRepository,
        RoleManager<AppRole> roleManager) : IJwtProvider
        {
        public async Task<string> CreateTokenAsync(AppUser user) // Bu kısımda token içerisine rol bilgisini ekleyebilmemiz için önce rol bilgisini çekmemiz lazım.
                                                // Bunun için AppUser sınıfına rol bilgisini eklememiz lazım. 
        {
            List<AppUserRole> appUserRoles = await userRoleRepository.Where(p => p.UserId == user.Id).ToListAsync();
            List<AppRole> roles = new();
            
            foreach(var userRole in appUserRoles)
            {
                AppRole? role = await roleManager.Roles.Where(p=> p.Id == userRole.RoleId).FirstOrDefaultAsync();
            if (role is not null)
            {
                roles.Add(role);
            }
        }

            List<string?> stringRoles = roles.Select(s => s.Name).ToList();
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim("UserName", user.UserName ?? string.Empty), 
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(stringRoles))
            };
            DateTime expires = DateTime.Now.AddDays(1); // Token 1 gün geçerli olacak.

            SymmetricSecurityKey securityKey = 
            new (Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value ?? ""));
            SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: configuration.GetSection("Jwt:Issuer").Value, // Kim oluşturdu.
                audience: configuration.GetSection("Jwt:Audience").Value, // Kim tarafından kullanılacak.
                claims: claims, // Body kısmında kullanıcı bu bilgileri açıp okuyabiliyor. Bu yüzden hassas bilgiler koymamak lazım.
                notBefore: DateTime.Now, // Token ne zamandan sonra kullanılacak bunu söyler. Mesela bu token oluşturulduğu andan itibaren kullanılabilir.
                expires: expires,
                signingCredentials: signingCredentials); // Uygulamanın şifrelencek anahtarını veriyoruz. Bu sayede tokenın değiştirilip değiştirilmediğini anlayabiliyoruz.
            JwtSecurityTokenHandler handler = new();
            string token = handler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
