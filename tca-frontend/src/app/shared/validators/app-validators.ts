import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class AppValidators {
  static password(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const valid =
        /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/.test(
          control.value
        );

      return valid ? null : { password: true };
    };
  }

  static confirmPassword(): ValidatorFn {
    return (form: AbstractControl): ValidationErrors | null => {
      const passwordConfirmControl = form.get('confirmPassword');
      const passwordControl = form.get('password');
      const passwordValue = passwordControl?.value;
      const confirmPasswordValue = passwordConfirmControl?.value;

      if (passwordValue !== confirmPasswordValue) {
        passwordConfirmControl?.setErrors({ passwordMismatch: true });
        return { passswordMismatch: true };
      } else {
        passwordConfirmControl?.setErrors(null);
        return null;
      }
    };
  }
}
