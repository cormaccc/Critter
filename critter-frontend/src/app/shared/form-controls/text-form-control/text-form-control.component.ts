import { CommonModule } from '@angular/common';
import { Component, HostBinding, Input } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-text-form-control',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './text-form-control.component.html',
  styleUrl: './text-form-control.component.scss',
})
export class TextFormControlComponent {
  @Input() control!: FormControl;
  @Input() label = '';
  @Input() placeholder = '';
  @Input() type: 'password' | 'text' = 'text';

  showPassword = false;

  protected toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }
}
