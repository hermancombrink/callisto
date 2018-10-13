import { TestBed } from '@angular/core/testing';

import { AudithistoryService } from './audithistory.service';

describe('AudithistoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AudithistoryService = TestBed.get(AudithistoryService);
    expect(service).toBeTruthy();
  });
});
