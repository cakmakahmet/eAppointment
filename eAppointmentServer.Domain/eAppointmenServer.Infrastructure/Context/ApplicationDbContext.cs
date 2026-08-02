using eAppointmentServer.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace eAppointmenServer.Infrastructure.Context //IdentityDbContext<AppUser,AppRole,Guid> olarak en başta inherit edip kullanım tipini belirledik. Bu yüzden DbSet kısmında tekrar tekrar eklememize gerek yok direkt bağlanmış oldu.
{
    internal sealed class ApplicationDbContext
        : IdentityDbContext<
            AppUser,
            AppRole,
            Guid,
            IdentityUserClaim<Guid>,
            AppUserRole,
            IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>,
            IdentityUserToken<Guid>>,
          IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) // DbContextOptions parametresi ile veritabanı bağlantı ayarlarını alıyoruz. Bu ayarlar, Program.cs dosyasında yapılacak.
        {

        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();  //Burada gördüğümüz gibi Identity framework'ü ile ilgili bazı tabloları kullanmak istemiyorsak,
                                                        //OnModelCreating metodunu override ederek, bu tabloları ignore edebiliriz. Bu tabloları kullanmak istemiyorsak, bu tabloların oluşturulmasını engellemek için ignore ediyoruz.
                                                        //builder.Entity<Doctor>().Property(p=> p.FirstName).HasColumnType("varchar(50))"); // Burada Doctor tablosundaki FirstName kolonunun tipini varchar(50) olarak belirliyoruz.
                                                        // Bu sayede veritabanında bu kolonun tipini değiştirebiliriz.
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Burada Assembly.GetExecutingAssembly() ile bu assembly'deki tüm IEntityTypeConfiguration implementasyonlarını bulup, OnModelCreating metoduna ekliyoruz.
                                                                                      // Bu sayede tüm entity configuration'larını tek tek eklemek yerine, bu metodu kullanarak tüm configuration'ları ekleyebiliriz.
        }
    }
}
