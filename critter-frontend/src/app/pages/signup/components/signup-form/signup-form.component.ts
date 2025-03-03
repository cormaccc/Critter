import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TextFormControlComponent } from '../../../../shared/form-controls/text-form-control/text-form-control.component';
import { AppValidators } from '../../../../shared/validators/app-validators';
import {
  AuthService,
  UserCreateInputDto,
  UserService,
} from '../../../../shared/services/service-proxies';
import { take } from 'rxjs';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-signup-form',
  standalone: true,
  imports: [ReactiveFormsModule, TextFormControlComponent, RouterModule],
  providers: [UserService],
  templateUrl: './signup-form.component.html',
  styleUrl: './signup-form.component.scss',
})
export class SignupFormComponent {
  constructor(private userService: UserService, private router: Router) {}

  form = new FormGroup(
    {
      username: new FormControl<string>('', [Validators.required]),
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

  public signup(): void {
    if (this.form.invalid) {
      return;
    }

    this.userService
      .createUser(
        new UserCreateInputDto({
          username: this.form.value.username ?? undefined,
          email: this.form.value.email ?? undefined,
          firstName: this.form.value.firstName ?? undefined,
          lastName: this.form.value.lastName ?? undefined,
          password: this.form.value.password ?? undefined,
        })
      )
      .pipe(take(1))
      .subscribe(() => {
        this.router.navigate(['login']);
      });
  }
}
