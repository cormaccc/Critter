import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { PostService } from '../../shared/services/service-proxies';
import { FeedComponent } from './components/feed/feed.component';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [CommonModule, FeedComponent],
  providers: [PostService],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent implements OnInit {
  constructor(private postsService: PostService) {}

  ngOnInit(): void {}
}
