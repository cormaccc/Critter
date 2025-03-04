import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { SignupComponent } from './pages/signup/signup.component';
import { authGuard } from './shared/guards/auth.guard';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { ComposePostComponent } from './pages/compose/post/compose-post.component';

export const routes: Routes = [
  { path: '', redirectTo: 'account/login', pathMatch: 'full' },
  {
    path: 'account',
    children: [
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'signup',
        component: SignupComponent,
      },
    ],
  },
  {
    path: 'feed',
    canActivate: [authGuard],
    component: LandingPageComponent,
  },
  {
    path: 'compose',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'post',
        pathMatch: 'full',
      },
      {
        path: 'post',
        component: ComposePostComponent,
      },
    ],
  },
];
