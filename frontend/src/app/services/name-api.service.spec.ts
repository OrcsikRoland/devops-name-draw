import { TestBed } from '@angular/core/testing';

import { NameApiService } from './name-api.service';

describe('NameApiService', () => {
  let service: NameApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NameApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
