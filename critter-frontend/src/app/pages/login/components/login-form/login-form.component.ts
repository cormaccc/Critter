import { Component } from '@angular/core';
import { TextFormControlComponent } from '../../../../shared/form-controls/text-form-control/text-form-control.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import {
  AuthService,
  LoginCommand,
  UserService,
} from '../../../../shared/services/service-proxies';
import { SubSink } from 'subsink';
import { catchError, debounce, debounceTime, throwError } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [CommonModule, RouterModule, TextFormControlComponent],
  providers: [AuthService, UserService],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss',
})
export class LoginFormComponent {
  private subs = new SubSink();
  protected incorrectLogin = false;
  form = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', Validators.required),
  });

  constructor(private authService: AuthService, private router: Router) {}

  protected login(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.authService
        .login(
          new LoginCommand({
            username: this.form.controls.email.value as string,
            password: this.form.controls.password.value as string,
            rememberMe: true,
          })
        )
        .pipe(
          debounceTime(1000),
          catchError((err) => {
            this.incorrectLogin = true;

            return throwError(err);
          })
        )
        .subscribe(() => {
          this.router.navigate(['feed']);
          console.clear();
        });
    }
  }
}
