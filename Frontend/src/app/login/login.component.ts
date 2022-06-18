import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { UserLoginForm } from 'models/userLoginForm';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: '',
      password: '',
    });

    this.form.valueChanges.subscribe(console.log);
  }

  clearForm() {
    this.form = this.fb.group({
      email: '',
      password: '',
    });
  }

  async submitHandler() {
    const formValue: UserLoginForm = this.form.value;
    console.warn(formValue);
    this.clearForm();
  }
}
