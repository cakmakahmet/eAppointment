using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IUserRoleRepository userRoleRepository) : IRequestHandler<GetAllUsersQuery,
    Result<List<GetAllUsersQueryResponse>>>
{
    public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        List<AppUser> users = await userManager.Users.OrderBy(p=> p.FirstName).ToListAsync(cancellationToken);
        List<GetAllUsersQueryResponse> response = 
            users.Select(s=> new GetAllUsersQueryResponse()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FirstName + " " + s.LastName,
                UserName = s.UserName,
                Email = s.Email
            }).ToList();
        foreach(var item in response)
        {
            List<AppUserRole> userRoles = await userRoleRepository.Where(p => p.UserId == item.Id).ToListAsync(cancellationToken);
            foreach(var UserRole in userRoles)
            {
                List<AppRole> roles =
                    await roleManager
                    .Roles
                    .Where(p => p.Id == UserRole.RoleId)
                    .ToListAsync(cancellationToken);
                List<Guid> stringRoles = roles.Select(s => s.Id).ToList();
                item.RoleIds = stringRoles;
              
            }
        }
        return response;
    }
}
