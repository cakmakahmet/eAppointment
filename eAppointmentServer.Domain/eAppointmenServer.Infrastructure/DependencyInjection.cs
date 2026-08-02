using eAppointmenServer.Infrastructure.Context;
using eAppointmenServer.Infrastructure.Repositories;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace eAppointmenServer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
            services.AddIdentity<AppUser, AppRole>(action =>
            { action.Password.RequiredLength = 1;
                action.Password.RequireUppercase = false;
                action.Password.RequireLowercase = false;
                action.Password.RequireNonAlphanumeric = false;
                action.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>(); // Identity framework'ü ile ilgili tüm işlemleri yapmamızı sağlar. AppUser ve AppRole sınıflarını generic parametre olarak veriyoruz. Guid ise primary key tipi olarak kullanıyoruz.
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
            services.Scan(action =>
            {
            action
            .FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime();
            });
            //services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            //services.AddScoped<IDoctorRepository, DoctorRepository>();
            //services.AddScoped<IPatientRepository, PatientRepository>();
                     // IUnitOfWork interface'ini ApplicationDbContext ile eşleştiriyoruz. Bu sayede IUnitOfWork üzerinden ApplicationDbContext'e erişebileceğiz.
            //services.AddScoped<IJwtProvider, JwtProvider>();
            return services; // User Manager üzerinden bir create işlemi yapıldığında, AppUser ve AppRole sınıflarını kullanarak veritabanına kaydedilecek. Bu yüzden AddEntityFrameworkStores metodunu çağırıyoruz.
        }
    }
}
