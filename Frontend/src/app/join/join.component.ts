import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { UserCreationForm } from 'models/userCreationForm';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-join',
  templateUrl: './join.component.html',
  styleUrls: ['./join.component.css'],
})
export class JoinComponent implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      displayName: '',
      email: '',
      password: '',
      passwordConfirmation: '',
    });

    this.form.valueChanges.subscribe(console.log);
  }

  clearForm() {
    this.form = this.fb.group({
      displayName: '',
      email: '',
      password: '',
      passwordConfirmation: '',
    });
  }

  async submitHandler() {
    const formValue: UserCreationForm = this.form.value;
    this.http
      .post(environment.apiEndpoint + '/Users', {
        displayName: formValue.displayName,
        email: formValue.email,
        password: formValue.password,
      })
      .subscribe((result) => {
        console.warn(result);
      });

    this.clearForm();
  }
}
