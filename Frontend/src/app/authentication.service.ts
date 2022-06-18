import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  fakeEmail: string = 'fakeemail@gmail.com';
  fakePassword: string = 'fakepassword';
  constructor() {}

  login(email: string, password: string): Observable<any> {
    // mock a successful call to api server
    if (email == this.fakeEmail && password == this.fakePassword) {
      localStorage.setItem(
        'kindSocialAuthToken',
        'my-super-secret-token-from-server'
      );
      return of(new HttpResponse({ status: 200 }));
    } else {
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
