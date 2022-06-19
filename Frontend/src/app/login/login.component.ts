import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { UserLoginForm } from 'models/userLoginForm';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: '',
      password: '',
    });

    this.form.valueChanges.subscribe(console.log);
  }

  clearForm() {
    this.ngOnInit();
  }

  async submitHandler() {
    const formValue: UserLoginForm = this.form.value;
    console.warn(formValue);
    this.authService.login(formValue.email, formValue.password);
    this.clearForm();
  }
}
