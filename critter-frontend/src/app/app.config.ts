import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authHeaderInterceptor } from './shared/interceptors/auth-header.interceptor';
import { API_BASE_URL } from './shared/services/service-proxies';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient(withInterceptors([authHeaderInterceptor])),
    { provide: API_BASE_URL, useValue: 'https://localhost:7162' },
  ],
};
