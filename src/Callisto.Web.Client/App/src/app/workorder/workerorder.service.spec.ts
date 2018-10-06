import { TestBed } from '@angular/core/testing';

import { WorkerorderService } from './workerorder.service';

describe('WorkerorderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WorkerorderService = TestBed.get(WorkerorderService);
    expect(service).toBeTruthy();
  });
});
