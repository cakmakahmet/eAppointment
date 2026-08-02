using TS.Result;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctor;
public sealed record GetAllDoctorsQuery() : IRequest<Result<List<Doctor>>>;
