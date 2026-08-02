using AutoMapper;
using eAppointmentServer.Application.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace eAppointment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        return services;
    }

}

