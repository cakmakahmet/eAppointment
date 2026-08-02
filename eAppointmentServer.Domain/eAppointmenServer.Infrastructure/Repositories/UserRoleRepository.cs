using eAppointmenServer.Infrastructure.Context;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.Infrastructure.Repositories;

using GenericRepository;

namespace eAppointmentServer.Infrastructure.Repositories;

internal sealed class UserRoleRepository
    : Repository<AppUserRole, ApplicationDbContext>,
      IUserRoleRepository
{
    public UserRoleRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}