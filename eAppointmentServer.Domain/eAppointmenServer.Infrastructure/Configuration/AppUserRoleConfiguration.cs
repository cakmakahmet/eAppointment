using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eAppointmentServer.Domain.Entities;


namespace eAppointmenServer.Infrastructure.Configuration;
    internal sealed class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.HasKey(k => new { k.UserId, k.RoleId });
    }

    }
   
