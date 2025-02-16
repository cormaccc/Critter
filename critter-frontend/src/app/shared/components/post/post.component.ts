import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { TimeSincePipe } from '../../pipes/time-since.pipe';
import { PostOutputDto, PostService } from '../../services/service-proxies';
import { map } from 'rxjs';
import { CustomRxJsOperators } from '../../operators/custom-rxjs-operators';
import { RepostModalComponent } from './components/repost-modal/repost-modal.component';
import { IRepostEvent } from './components/repost-modal/repost-modal.model';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [CommonModule, TimeSincePipe, RepostModalComponent],
  providers: [PostService],
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PostComponent {
  constructor(
    private postService: PostService,
    private cd: ChangeDetectorRef
  ) {}
  @Input() post!: PostOutputDto;
  @Output() openRepostModal = new EventEmitter<IRepostEvent>();

  private operators: CustomRxJsOperators = new CustomRxJsOperators(
    this.postService
  );

  protected likePost(postId: number): void {
    this.postService
      .likePost(postId)
      .pipe(
        this.operators.updatePost(postId),
        map((result) => (this.post = result))
      )
      .subscribe(() => this.cd.detectChanges());
  }

  protected unlikePost(postId: number): void {
    debugger;
    this.postService
      .unlikePost(postId)
      .pipe(
        this.operators.updatePost(postId),
        map((result) => (this.post = result))
      )
      .subscribe(() => this.cd.detectChanges());
  }

  protected repost() {
    if (this.post.postId)
      this.openRepostModal.emit({
        isReposted: !!this.post.hasReposted,
        postId: this.post.postId,
      });
  }
}
