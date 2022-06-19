import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Post } from 'models/Post';
import { environment } from 'src/environments/environment';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import * as moment from 'moment';
import { PostCreationForm } from 'models/PostCreationForm';
import { User } from 'models/User';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css'],
})
export class PostsComponent implements OnInit {
  form!: FormGroup;
  posts!: Post[];
  users!: User[];
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
      setTimeout(() => {
        this.loading = false;
      }, 1000);
    } catch (error) {
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

  getAuthor(post: Post) {
    let authorDisplayName;
    for (let user of this.users) {
      if (user.id === post.authorId) {
        authorDisplayName = user.displayName;
      }
    }
    return authorDisplayName;
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
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
        },
        (reason) => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
