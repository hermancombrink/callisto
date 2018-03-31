import { TestBed, inject } from '@angular/core/testing';

import { PowersampleService } from './powersample.service';

describe('PowersampleServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PowersampleService]
    });
  });

  it('should be created', inject([PowersampleService], (service: PowersampleService) => {
    expect(service).toBeTruthy();
  }));
});
