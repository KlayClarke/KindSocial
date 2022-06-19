import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from 'models/Post';
import { environment } from 'src/environments/environment';

import * as moment from 'moment';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css'],
})
export class PostsComponent implements OnInit {
  posts!: Post[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // to get posts from mongodb and populate 'posts' array
    this.http.get(environment.apiEndpoint + '/Posts').subscribe((response) => {
      this.posts = response as Post[];
    });
  }

  momentTimestamp(time: string) {
    return moment(time).fromNow();
  }
}
