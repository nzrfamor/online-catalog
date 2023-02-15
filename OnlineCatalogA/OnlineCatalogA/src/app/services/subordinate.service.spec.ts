import { TestBed } from '@angular/core/testing';

import { SubordinateService } from './subordinate.service';

describe('SubordinateService', () => {
  let service: SubordinateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubordinateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
