import { Component, OnInit } from '@angular/core';
import { UserImageComponent } from '../../../shared/components/user-image/user-image.component';
import { CommonModule, Location } from '@angular/common';
import {
  PostCreateInputDto,
  PostOutputDto,
  PostService,
} from '../../../shared/services/service-proxies';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { EMPTY, Observable, switchMap, take } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { PostComponent } from '../../../shared/components/post/post.component';

@Component({
  selector: 'app-compose-post',
  standalone: true,
  imports: [
    CommonModule,
    UserImageComponent,
    PostComponent,
    ReactiveFormsModule,
  ],
  providers: [PostService],
  templateUrl: './compose-post.component.html',
  styleUrl: './compose-post.component.scss',
})
export class ComposePostComponent implements OnInit {
  post$!: Observable<PostOutputDto>;
  newPostForm = new FormGroup({
    post: new FormControl<string>('', [Validators.required]),
  });

  get hasValue(): boolean {
    return !!this.newPostForm.controls.post?.value;
  }
  constructor(
    private location: Location,
    private postService: PostService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.post$ = this.activatedRoute.queryParams.pipe(
      switchMap((params) => {
        debugger;
        if (params.postId) return this.postService.getPost(params.postId);
        else return EMPTY;
      })
    );
  }

  navigateBack() {
    this.location.back();
  }

  createPost() {
    if (this.newPostForm.valid)
      this.postService
        .createPost(
          new PostCreateInputDto({
            body: this.newPostForm.controls.post?.value ?? undefined,
          })
        )
        .pipe(take(1))
        .subscribe(() => {
          this.navigateBack();
        });
  }
}
