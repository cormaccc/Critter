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

  repost(): void {
    this.repostEvent.emit();
  }

  unrepost(): void {
    this.unrepostEvent.emit();
  }

  closeModal(): void {
    this.dialogRef.close();
  }
}
