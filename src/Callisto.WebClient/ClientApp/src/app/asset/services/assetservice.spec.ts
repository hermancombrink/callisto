import { TestBed, inject } from '@angular/core/testing';

import { AssetService } from './asset.service';

describe('AssetServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AssetService]
    });
  });

  it('should be created', inject([AssetService], (service: AssetService) => {
    expect(service).toBeTruthy();
  }));
});
