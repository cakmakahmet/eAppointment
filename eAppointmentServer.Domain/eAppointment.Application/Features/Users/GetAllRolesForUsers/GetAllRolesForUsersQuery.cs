using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;
using Microsoft.EntityFrameworkCore;

namespace eAppointmentServer.Application.Features.Users.GetAllRolesForUsers;

public sealed record GetAllRolesForUsersQuery() : IRequest<Result<List<AppRole>>>;

internal sealed class GetAllRolesForUsersQueryHandler(
    RoleManager<AppRole> roleManager) : IRequestHandler<GetAllRolesForUsersQuery,Result<List<AppRole>>>
{
    public async Task<Result<List<AppRole>>> Handle(GetAllRolesForUsersQuery request, CancellationToken cancellationToken)
    {
        List<AppRole> roles = await roleManager.Roles.OrderBy(p => p.Name).ToListAsync(cancellationToken);
        return roles;
    }
}
