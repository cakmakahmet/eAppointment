import { Component } from '@angular/core';
import { Router,RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  constructor(
    private router: Router,
    public auth: AuthService
  ){}
  signOut(){
    localStorage.removeItem("token");
    this.router.navigateByUrl("/login");
  }
  ngOnInit(): void {
  this.auth.isAuthenticated();
}
}
