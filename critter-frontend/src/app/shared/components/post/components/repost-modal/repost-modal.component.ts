import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-repost-modal',
  standalone: true,
  imports: [CommonModule, MatDialogModule],
  templateUrl: './repost-modal.component.html',
  styleUrl: './repost-modal.component.scss',
})
export class RepostModalComponent {
  private dialogRef = inject(MatDialogRef);
  public isReposted = false;

  repostEvent = new EventEmitter();
  unrepostEvent = new EventEmitter();
  quoteEvent = new EventEmitter();

  handleRepostClick(): void {
    if (this.isReposted) {
      this.unrepostEvent.emit();
    } else {
      this.repostEvent.emit();
    }
  }

  quote(): void {
    this.quoteEvent.emit();
  }

  closeModal(): void {
    this.dialogRef.close();
  }
}
