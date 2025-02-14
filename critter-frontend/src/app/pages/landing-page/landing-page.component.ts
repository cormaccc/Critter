import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { PostService } from '../../shared/services/service-proxies';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [CommonModule],
  providers: [PostService],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent implements OnInit {
  constructor(private postsService: PostService) {}

  ngOnInit(): void {
    this.postsService.getPost(5).subscribe((result) => console.log(result));
  }
}
