import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import {
  FeedService,
  GetFeedInputDto,
  PostOutputDto,
  PostService,
} from '../../../../shared/services/service-proxies';
import { BehaviorSubject, Observable, Subject, switchMap, take } from 'rxjs';
import { PostComponent } from '../../../../shared/components/post/post.component';
import { RepostModalComponent } from '../../../../shared/components/post/components/repost-modal/repost-modal.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { IRepostEvent } from '../../../../shared/components/post/components/repost-modal/repost-modal.model';
import { ConsoleLogPipe } from '../../../../shared/pipes/console-log/console-log.pipe';
import { Router } from '@angular/router';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [CommonModule, PostComponent, ConsoleLogPipe, MatDialogModule],
  providers: [FeedService, PostService],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FeedComponent implements OnInit {
  postsSubject = new BehaviorSubject<PostOutputDto[]>([]);
  matDialog = inject(MatDialog);

  get posts(): Observable<PostOutputDto[]> {
    return this.postsSubject.asObservable();
  }

  constructor(
    private feedService: FeedService,
    private postsService: PostService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getPostsForFeed();
  }

  openRepostModal(event: IRepostEvent): void {
    const dialogRef = this.matDialog.open<RepostModalComponent>(
      RepostModalComponent,
      {
        height: '200px',
        position: {
          bottom: '0',
        },
        panelClass: ['app-dialog', 'repost'],
      }
    );

    dialogRef.componentInstance.isReposted = event.isReposted;

    if (!event.isReposted)
      dialogRef.componentInstance.repostEvent
        .pipe(
          take(1),
          switchMap(() => this.postsService.repostPost(event.postId))
        )
        .subscribe(() => {
          this.getPostsForFeed();
          dialogRef.close();
        });

    if (event.isReposted) {
      dialogRef.componentInstance.unrepostEvent
        .pipe(
          take(1),
          switchMap(() => this.postsService.unrepostPost(event.postId))
        )
        .subscribe(() => {
          this.getPostsForFeed();
          dialogRef.close();
        });
    }
    dialogRef.componentInstance.quoteEvent.subscribe(() => {
      this.router.navigate(['compose', 'post'], {
        queryParams: { postId: event.postId },
      });
      dialogRef.close();
    });
  }

  getPostsForFeed() {
    this.feedService
      .getFeed(new GetFeedInputDto({ take: 10, skip: 0, pageIndex: 0 }))
      .pipe(take(1))
      .subscribe((posts) => {
        this.postsSubject.next(posts);
      });
  }

  navigateToCompose() {
    this.router.navigate(['compose', 'post']);
  }
}
