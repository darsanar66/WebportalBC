import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      UserName: '',
      Email: '',
      Password: ''
    });
  }

  ngOnInit(): void {
  }

  submit(): void {
    this.http.post<any>('http://localhost:5202/api/Auth/login', this.form.value, {
      withCredentials: true
    }).subscribe(
      (response) => {
        if (response && response.token) {
          localStorage.setItem('token', response.token);
          this.router.navigate(['/']); // Navigate to home page on successful login
        } else {
          console.error('Invalid response from server:', response);
        }
      },
      (error) => {
        console.error('Error during login:', error);
        // Handle error appropriately (e.g., display error message to user)
      }
    );
  }
}
