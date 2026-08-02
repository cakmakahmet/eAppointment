using eAppointmentServer.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace eAppointmentServer.Application;
public static class Constants
{
    public static List<AppRole> GetRoles()
    {
        List<string> roles = new()
        {
            "Admin",
            "Doctor",
            "Personel"
        };
        return roles.Select(s=> new AppRole() { Name = s }).ToList();
    }
}


