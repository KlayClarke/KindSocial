import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { PostsComponent } from './posts/posts.component';
import { ExploreComponent } from './explore/explore.component';
import { MessagesComponent } from './messages/messages.component';
import { SettingsComponent } from './settings/settings.component';
import { LoginComponent } from './login/login.component';
import { JoinComponent } from './join/join.component';
import { AuthenticationGuardGuard } from './authentication-guard.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    PostsComponent,
    ExploreComponent,
    MessagesComponent,
    SettingsComponent,
    LoginComponent,
    JoinComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: PostsComponent },
      { path: 'explore', component: ExploreComponent },
      { path: 'join', component: JoinComponent },
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'messages',
        component: MessagesComponent,
        canActivate: [AuthenticationGuardGuard],
      },
      {
        path: 'settings',
        component: SettingsComponent,
        canActivate: [AuthenticationGuardGuard],
      },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
