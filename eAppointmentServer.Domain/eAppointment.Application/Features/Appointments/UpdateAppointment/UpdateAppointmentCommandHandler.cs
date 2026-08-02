using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.UpdateAppointment;

internal sealed class UpdateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateAppointmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateAppointmentCommand request, CancellationToken
      cancellationToken)
    {
        DateTime startDate = Convert.ToDateTime(request.startDate);
        DateTime endDate = Convert.ToDateTime(request.endDate);

        Appointment? appointment = await appointmentRepository
            .GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (appointment == null)
        {
            return Result<string>.Failure("Appointment not found.");
        }
        //Kontrol şartı yazıyoruz.
        bool isAppointmentDateNotAvaiable = await appointmentRepository
            .AnyAsync
            (p => p.DoctorId == appointment.DoctorId &&
            ((p.StartDate < endDate && p.StartDate >= startDate) || //Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çaışıyor.
            (p.EndDate > startDate && p.EndDate <= endDate) || //Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor.
            (p.StartDate >= startDate && p.EndDate <= endDate) || //Mevcut randevu tamamen diğer randevunun içinde.
            (p.StartDate <= startDate && p.EndDate >= endDate)), //Mevcut randevu, diğer randevuyu tamamen kapsıyor.
            cancellationToken);

        if (isAppointmentDateNotAvaiable)
        {
            return Result<string>.Failure("Appointment date is not avaiable.");
        }

        appointment.StartDate = Convert.ToDateTime(request.startDate);
        appointment.EndDate = Convert.ToDateTime(request.endDate);
        //Yukarda tracking yaptığımız için buraya tekrar update eklememize gerek kalmıyor await'le. Tracking zaten update'i işaretliyor.
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Appointment update is successfull";
    } 
}
