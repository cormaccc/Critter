import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TextFormControlComponent } from '../../../../shared/form-controls/text-form-control/text-form-control.component';
import { AppValidators } from '../../../../shared/validators/app-validators';

@Component({
  selector: 'app-signup-form',
  standalone: true,
  imports: [ReactiveFormsModule, TextFormControlComponent],
  templateUrl: './signup-form.component.html',
  styleUrl: './signup-form.component.scss',
})
export class SignupFormComponent {
  form = new FormGroup(
    {
      firstName: new FormControl<string>('', [Validators.required]),
      lastName: new FormControl<string>('', [Validators.required]),
      email: new FormControl<string>('', [
        Validators.required,
        Validators.email,
      ]),
      password: new FormControl<string>('', [
        Validators.required,
        AppValidators.password(),
      ]),
      confirmPassword: new FormControl<string>('', [Validators.required]),
    },
    [AppValidators.confirmPassword()]
  );

  get isFormValid(): boolean {
    return this.form.valid;
  }
}
