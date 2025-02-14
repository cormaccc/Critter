import { Component } from '@angular/core';
import { TextFormControlComponent } from '../../../../shared/form-controls/text-form-control/text-form-control.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
  API_BASE_URL,
  AuthService,
  LoginCommand,
} from '../../../../shared/services/service-proxies';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [RouterModule, TextFormControlComponent],
  providers: [AuthService],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss',
})
export class LoginFormComponent {
  private subs = new SubSink();
  form = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', Validators.required),
  });

  constructor(private authService: AuthService) {}

  protected login(): void {
    this.form.markAllAsTouched();
    debugger;
    if (this.form.valid) {
      this.authService
        .login(
          new LoginCommand({
            username: this.form.controls.email.value as string,
            password: this.form.controls.password.value as string,
            rememberMe: true,
          })
        )
        .subscribe((response) => console.log(response));
    }
  }
}
