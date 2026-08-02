import { Component, ViewChild,ElementRef} from '@angular/core';
import { departments } from '../../constants';
import { Doctors } from '../doctors/doctors';
import { DoctorModel } from '../../models/doctor.model';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import {DxSchedulerModule} from 'devextreme-angular';
import { HttpService } from '../../services/http';
import { AppointmentModel } from '../../models/appointment.model';
import { CreateAppointmentModel } from '../../models/create-appointment-model';
import { FormValidateDirective } from 'form-validate-angular';
import { PatientModel } from '../../models/patient.model';
import { SwalService } from '../../services/swal';

declare const $:any;

@Component({
  selector: 'app-home',
  imports: [FormsModule,CommonModule,DxSchedulerModule,FormValidateDirective],
  templateUrl: './home.html',
  styleUrl: './home.css',
  providers: [DatePipe]
})
export class Home {
  departments = departments;
  doctors: DoctorModel[] = [];

  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  selectedDepartmentValue: number = 0;
  selectedDoctorId: string = "";

  appointments: AppointmentModel[] = []
  createModel: CreateAppointmentModel = new CreateAppointmentModel();

  constructor(
    private http: HttpService,
    private date: DatePipe,
    private swal: SwalService
  ){}

  getAllDoctor(){
    this.selectedDoctorId = "";
    if(this.selectedDepartmentValue > 0){
      this.http.post<DoctorModel[]>("Appointments/GetAllDoctorByDepartment",
        {departmentValue: +this.selectedDepartmentValue}, (res)=>{
        this.doctors = res.data;
      });
    }
  }

  getAllAppointments(){
    if(this.selectedDoctorId){
      this.http.post<AppointmentModel[]>("Appointments/GetAllByDoctorId",
        {doctorId: this.selectedDoctorId}, (res)=> {
          this.appointments = res.data;
        });
    }
  }

  onAppointmentFormOpening(e: any){
    e.cancel = true;
    this.createModel.startDate =  this.date.transform(e.appointmentData.startDate, "dd.MM.yyyy HH:mm") ?? "";
    this.createModel.endDate =  this.date.transform(e.appointmentData.endDate, "dd.MM.yyyy HH:mm") ?? "";
    this.createModel.doctorId = this.selectedDoctorId;

    $("#addModal").modal("show");
  }

  getPatient(){
    this.http.post<PatientModel>("Appointments/GetPatientByIdentityNumber", {identityNumber: this.createModel
      .identityNumber}, res => {
        if(res.data === null){
        this.createModel.patientId = null;
        this.createModel.firstName = "";
        this.createModel.lastName = "";
        this.createModel.city = "";
        this.createModel.town = "";
        this.createModel.fullAdress = "";
        return;
        }
        this.createModel.patientId = res.data.id;
        this.createModel.firstName = res.data.firstName;
        this.createModel.lastName = res.data.lastName;
        this.createModel.city = res.data.city;
        this.createModel.town = res.data.town;
        this.createModel.fullAdress = res.data.fullAdress;
    } )
  }

  create(form: NgForm){
    if(form.valid){
      this.http.post<string>("Appointments/Create", this.createModel, res => {
        this.swal.callToast(res.data);
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel = new CreateAppointmentModel();
        this.getAllAppointments();
      }
      )

    }
  }

  onAppointmentDeleted(e: any){
    e.cancel = true;

  }
  onAppointmentDeleting(e: any){
    e.cancel = true;

    this.swal.callSwal("Delete appointment?",
      `You want to delete ${e.appointmentData.patient.fullName} appointment?`,()=>
    {
        this.http.post<string>("Appointments/DeleteById", {id:e.appointmentData.id}, res => {
        this.swal.callToast(res.data, "info");
        this.getAllAppointments();
      });

    })
  }
  onAppointmentUpdating(e: any){
    e.cancel = true;   // Taşıma işlemi başarısızsa taşıma işlemini yapamasın diye bunu ekliyoruz. Taşıdığını sanmayalım.
    const data = {
      id: e.oldData.id,
      startDate: e.newData.startDate,
      endDate: e.newData.endDate,

    };

    this.http.post("Appointments/Update", data, res=> {
      this.swal.callToast(res.data);
      this.getAllAppointments();
  });
  }
}
