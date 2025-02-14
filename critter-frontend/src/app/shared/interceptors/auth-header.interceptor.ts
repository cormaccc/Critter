import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map, throwError } from 'rxjs';

export const authHeaderInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const clonedRequest = req.clone({
    withCredentials: true,
  });

  return next(clonedRequest).pipe(
    catchError((error) => {
      debugger;
      if (error?.status === 401) {
        router.navigate(['account', 'login']);
      }

      return throwError(() => error);
    })
  );
};
