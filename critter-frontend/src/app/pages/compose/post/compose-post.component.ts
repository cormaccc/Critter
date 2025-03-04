import { Component } from '@angular/core';
import { UserImageComponent } from '../../../shared/components/user-image/user-image.component';
import { Location } from '@angular/common';
import { PostCreateInputDto, PostService } from '../../../shared/services/service-proxies';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-compose-post',
  standalone: true,
  imports: [UserImageComponent, ReactiveFormsModule],
  providers: [PostService],
  templateUrl: './compose-post.component.html',
  styleUrl: './compose-post.component.scss',
})
export class ComposePostComponent {
  newPostForm = new FormGroup({
    post: new FormControl<string>('', [Validators.required])
  })

  get hasValue(): boolean {
    return !!this.newPostForm.controls.post?.value
  }
  constructor(private location: Location, private postService: PostService) {}

  navigateBack() {
    this.location.back();
  }

  createPost() {
    if (this.newPostForm.valid)
      this.postService.createPost(new PostCreateInputDto({
        body: this.newPostForm.controls.post?.value ?? undefined
      }))
  }
}
