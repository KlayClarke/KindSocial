import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  currentRoute: string | undefined;
  constructor(private router: Router) {}

  ngOnInit(): void {
    this.currentRoute = this.router.url.includes('explore')
      ? 'explore'
      : this.router.url.includes('messages')
      ? 'messages'
      : this.router.url.includes('settings')
      ? 'settings'
      : 'home';
    console.log(this.currentRoute);
  }
}

//TODO: highlight link of current route in navbar
//TODO: change current route variable when route changes
