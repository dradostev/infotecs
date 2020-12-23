import { TestBed } from '@angular/core/testing';

import { SignalService } from './signal.service';

describe('SignalService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SignalService = TestBed.get(SignalService);
    expect(service).toBeTruthy();
  });
});
