import { TestBed } from '@angular/core/testing';

import { NameFacadeService } from './name-facade.service';

describe('NameFacadeService', () => {
  let service: NameFacadeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NameFacadeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
