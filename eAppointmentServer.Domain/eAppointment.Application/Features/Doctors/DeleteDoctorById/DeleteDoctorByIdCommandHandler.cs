using eAppointmentServer.Domain.Repositories;
using MediatR;
using TS.Result;
using GenericRepository;
using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById;

internal sealed class DeleteDoctorByIdCommandHander(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDoctorByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
        if(doctor is null)
        {
            return Result<string>.Failure("Doctor not found");
        }
        doctorRepository.Delete(doctor);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Doctor delete is succesful";
    }
}

    
    

