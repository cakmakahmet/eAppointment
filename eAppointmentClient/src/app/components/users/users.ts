import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { UserModel } from '../../models/user.model';
import { HttpService } from '../../services/http';
import { SwalService } from '../../services/swal';
import { UserPipe } from '../pipe/user-pipe';
import { PatientPipe } from '../pipe/patient-pipe';
import { RoleModel } from '../../models/role.model';

@Component({
  selector: 'app-users',
  imports: [CommonModule, FormsModule, FormValidateDirective, UserPipe, RouterLink],
  templateUrl: './users.html',
  styleUrl: './users.css',
})
export class Users {
  users: UserModel[]=[];
  roles: RoleModel[]=[];

  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;


  createModel: UserModel = new UserModel();
  updateModel: UserModel = new UserModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ){}


  search: string= "";


  ngOnInit(): void {
    this.getAll();
    this.getAllRoles();
  }

  getAll(){
    this.http.post<UserModel[]>("Users/GetAll", {}, (res)=> {
      this.users = res.data;
    })
  }

  getAllRoles(){
    this.http.post<RoleModel[]>("Users/GetAllRoles", {}, (res)=> {
      this.roles = res.data;
    })
  }

  add(form: NgForm){
    if(form.valid){
      this.http.post<string>("Users/Create", this.createModel,(res)=>{
        this.swal.callToast(res.data,"success");
        this.getAll();
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel = new UserModel();
      });
    }
  }

  delete(id: string, fullName: string){
    this.swal.callSwal("Delete user?", `You want to delete ${fullName}?`,()=> {
      this.http.post<string>("Users/DeleteById", {id:id}, (res)=> {
        this.swal.callToast(res.data);
        this.getAll();
      })
    })
  }

  get(data: UserModel){
    this.updateModel = {...data};
  }

  update(form: NgForm){
        if(form.valid){
      this.http.post<string>("Users/Update", this.updateModel,(res)=>{
        this.swal.callToast(res.data,"success");
        this.getAll();
        this.updateModalCloseBtn?.nativeElement.click();
      });
    }

  }
}


