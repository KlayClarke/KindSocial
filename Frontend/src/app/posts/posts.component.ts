import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Post } from 'models/Post';
import { environment } from 'src/environments/environment';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import * as moment from 'moment';
import { PostCreationForm } from 'models/PostCreationForm';
import { User } from 'models/User';
import { Comment } from 'models/Comment';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css'],
})
export class PostsComponent implements OnInit {
  form!: FormGroup;
  users!: User[];
  posts!: Post[];
  comments!: Comment[];
  closeResult = '';
  loading: boolean = false;

  constructor(
    private http: HttpClient,
    private modalService: NgbModal,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    // initialize form
    this.form = this.fb.group({
      title: '',
      body: '',
      authorId: '',
    });
    this.form.valueChanges.subscribe(console.log);
    this.populateState();
  }

  async populateState() {
    this.loading = true;
    try {
      // to get users from mongodb and populate 'users' array
      this.http
        .get(environment.apiEndpoint + '/Users')
        .subscribe((response) => {
          this.users = response as User[];
          console.log(response);
        });
      // to get posts from mongodb and populate 'posts' array
      this.http
        .get(environment.apiEndpoint + '/Posts')
        .subscribe((response) => {
          this.posts = response as Post[];
        });
      // to get comments from mongodb and populate 'comments' array
      this.http
        .get(environment.apiEndpoint + '/Comments')
        .subscribe((response) => {
          this.comments = response as Comment[];
        });
      // show loading spinner animation for 1 second after fetching successfully ends
      setTimeout(() => {
        this.loading = false;
      }, 1000);
    } catch (error) {
      // if error, console warn error
      console.warn(error);
    }
  }

  // helper method to format posts dateCreated param

  momentTimestamp(time: string) {
    return moment(time).fromNow();
  }

  // method to clear form

  clearForm() {
    this.form = this.fb.group({
      title: '',
      body: '',
      authorId: '',
    });
  }

  // helper method to get post author

  getPostAuthor(post: Post): string {
    let authorDisplayName;
    for (let user of this.users) {
      if (user.id === post.authorId) {
        authorDisplayName = user.displayName;
      }
    }
    return authorDisplayName as string;
  }

  // helper method to get post comments

  getComments(post: Post): Comment[] {
    let comments = [];
    for (let comment of this.comments) {
      if (comment.postId === post.id) {
        comments.push(comment);
      }
    }
    return comments as Comment[];
  }

  // helper method to get comment author

  getCommentAuthor(comment: Comment): string {
    let authorDisplayName;
    for (let user of this.users) {
      if (user.id === comment.authorId) {
        authorDisplayName = user.displayName;
      }
    }
    return authorDisplayName as string;
  }

  // post creation handle

  async submitHandler() {
    const formValue: PostCreationForm = this.form.value;
    this.http
      .post(environment.apiEndpoint + '/Posts', {
        title: formValue.title,
        body: formValue.body,
        // dateCreated: Date().toString(),
        dateCreated: new Date().toISOString(),
        authorId: formValue.authorId,
      })
      .subscribe((result) => {
        console.warn(result);
      });
    this.clearForm();
  }

  // post creation modal functionality below

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }
}
