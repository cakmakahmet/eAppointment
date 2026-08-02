using eAppointmentServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eAppointmentServer.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,Guid> // IdentityDbContext sınıfını miras alıyoruz. Bu sınıf, Identity framework'ü ile ilgili tüm işlemleri yapmamızı sağlar. AppUser ve AppRole sınıflarını generic parametre olarak veriyoruz. Guid ise primary key tipi olarak kullanıyoruz.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) // DbContextOptions parametresi ile veritabanı bağlantı ayarlarını alıyoruz. Bu ayarlar, Program.cs dosyasında yapılacak.
        {

        }
    }
}
