using eAppointmentServer.Domain.Entities;
using GenericRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAppointmentServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Update.Internal;


namespace eAppointmentServer.Application.Services
{
    public interface IJwtProvider
    {
        Task<string> CreateTokenAsync(AppUser user); // Kullanıcıya ait bilgileri isteyecek ve JWT token oluşturacak bir metot.
    }
}
