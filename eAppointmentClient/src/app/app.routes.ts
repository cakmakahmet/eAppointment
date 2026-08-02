import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Layouts } from './components/layouts/layouts';
import { Home } from './components/home/home';
import { NotFound } from './components/not-found/not-found';
import { inject } from '@angular/core';
import { AuthService } from './services/auth';
import { Doctors} from './components/doctors/doctors';
import { PatientsComponent } from './components/patients/patients';
import { Users } from './components/users/users';

export const routes: Routes = [
  {
    path:"login",
    component: Login
  },
  {
    path: "",
    component: Layouts,
    canActivateChild:[()=> inject(AuthService).isAuthenticated()],
    children:[
      {
        path: "",
        component: Home
      },
      {
        path: "doctors",
        component: Doctors
      },
      {
        path: "patients",
        component: PatientsComponent
      },
      {
        path: "users",
        component: Users
      }
    ]
  },
  {
    path: "**",
    component: NotFound
  }
];
