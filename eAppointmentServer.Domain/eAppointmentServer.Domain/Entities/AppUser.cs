namespace eAppointmentServer.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

public sealed class AppUser : IdentityUser<Guid>
    {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join("", FirstName, LastName);

    }

