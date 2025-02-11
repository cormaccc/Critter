import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthServiceProxyService {
  private baseUrl = 'https://localhost:7162/';

  constructor(private http: HttpClient) {}
}
