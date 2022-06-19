import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  fakeEmail: string = 'fakeemail@gmail.com';
  fakePassword: string = 'fakepassword';
  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    // mock a successful call to api server
    if (email == this.fakeEmail && password == this.fakePassword) {
      // localStorage.setItem(
      //   'kindSocialAuthToken',
      //   'my-super-secret-token-from-server'
      // );

      // send get request for user in db with email and password that matches
      // if matches, create global user state
      console.warn('logged in');
      return of(new HttpResponse({ status: 200 }));
    } else {
      console.warn('wrong credentials');
      return of(new HttpResponse({ status: 401 }));
    }
  }

  logout(): void {
    localStorage.removeItem('kindSocialAuthToken');
  }

  isUserLoggedIn(): boolean {
    if (localStorage.getItem('kindSocialAuthToken') != null) {
      return true;
    }

    return false;
  }
}
