import { TestBed } from '@angular/core/testing';

import { AuthServiceProxyService } from './auth-service-proxy.service';

describe('AuthServiceProxyService', () => {
  let service: AuthServiceProxyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthServiceProxyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
