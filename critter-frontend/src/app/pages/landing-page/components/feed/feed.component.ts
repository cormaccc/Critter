import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import {
  FeedService,
  GetFeedInputDto,
  PostOutputDto,
  PostService,
} from '../../../../shared/services/service-proxies';
import { Observable, switchMap, take } from 'rxjs';
import { TimeSincePipe } from '../../../../shared/pipes/time-since.pipe';
import { PostComponent } from '../../../../shared/components/post/post.component';
import { RepostModalComponent } from '../../../../shared/components/post/components/repost-modal/repost-modal.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { IRepostEvent } from '../../../../shared/components/post/components/repost-modal/repost-modal.model';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [CommonModule, PostComponent, MatDialogModule],
  providers: [FeedService, PostService],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.scss',
})
export class FeedComponent implements OnInit {
  posts$: Observable<PostOutputDto[]> = new Observable();
  matDialog = inject(MatDialog);

  constructor(
    private feedService: FeedService,
    private postsService: PostService
  ) {}

  ngOnInit(): void {
    this.posts$ = this.feedService.getFeed(
      new GetFeedInputDto({ take: 10, skip: 0, pageIndex: 0 })
    );
  }

  openRepostModal(event: IRepostEvent): void {
    debugger;
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
        .subscribe();

    if (event.isReposted)
      dialogRef.componentInstance.unrepostEvent
        .pipe(
          take(1),
          switchMap(() => this.postsService.unrepostPost(event.postId))
        )
        .subscribe();
  }
}
