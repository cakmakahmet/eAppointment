using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAppointmentServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using eAppointmentServer.Domain.Enums;

namespace eAppointmentServer.Infrastructure.Configuration
{
    internal sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(p=> p.FirstName).HasColumnType("varchar(50)");
            builder.Property(p => p.LastName).HasColumnType("varchar(50)");
            //builder.HasIndex(x => x.FirstName).IsUnique(); // Bu satır , FirstName alanına benzersiz bir indeks ekler. Bu, aynı FirstName değerine sahip birden fazla doktorun veritabanında bulunmasını engeller.
            //Ancak, genellikle doktorlar aynı adı paylaşabilir, bu yüzden bu satırı yorum satırı olarak bırakmak mantıklı olabilir.
            builder.Property(p => p.Department)
                .HasConversion(v => v.Value, v => DepartmentEnum.FromValue(v))
                .HasColumnName("Department");
        }
    }
}


