import { departments } from '../../constants';
import { Component, OnInit, ElementRef } from '@angular/core';
import {Router, RouterLink } from '@angular/router';
import { HttpService } from '../../services/http';
import { DoctorModel } from '../../models/doctor.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SwalService } from '../../services/swal';
import { DoctorPipe } from '../pipe/doctor-pipe';

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [CommonModule,RouterLink,FormsModule,FormValidateDirective, DoctorPipe],
  templateUrl: './doctors.html',
  styleUrl: './doctors.css',
})
export class Doctors implements OnInit {
  doctors: DoctorModel[]=[];
  departments = departments;

  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;


  createModel: DoctorModel = new DoctorModel();
  updateModel: DoctorModel = new DoctorModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ){}


  search: string= "";


  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this.http.post<DoctorModel[]>("Doctors/GetAll", {}, (res)=> {
      this.doctors = res.data;
    })
  }

  add(form: NgForm){
    if(form.valid){
      this.http.post<string>("Doctors/Create", this.createModel,(res)=>{
        this.swal.callToast(res.data,"success");
        this.getAll();
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel = new DoctorModel();
      });
    }
  }

  delete(id: string, fullName: string){
    this.swal.callSwal("Delete doctor?", `You want to delete ${fullName}?`,()=> {
      this.http.post<string>("Doctors/DeleteById", {id:id}, (res)=> {
        this.swal.callToast(res.data);
        this.getAll();
      })
    })
  }

  get(data: DoctorModel){
    this.updateModel = {...data};
    this.updateModel.departmentValue = data.department.value;
  }

  update(form: NgForm){
        if(form.valid){
      this.http.post<string>("Doctors/Update", this.updateModel,(res)=>{
        this.swal.callToast(res.data,"success");
        this.getAll();
        this.updateModalCloseBtn?.nativeElement.click();
      });
    }

  }
}
