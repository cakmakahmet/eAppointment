using MediatR;
using TS.Result;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctor;

internal sealed class GetAllDoctorsQueryHandler(
    IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorsQuery, Result<List<Doctor>>>
{
    public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        List<Doctor> doctors = await doctorRepository
             .GetAll()
             .OrderBy(p => p.Department)
             .ThenBy(p => p.FirstName)
             .ToListAsync(cancellationToken);
        return doctors;
    }
}
