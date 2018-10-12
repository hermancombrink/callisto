import { TestBed } from '@angular/core/testing';

import { DatasourceFactoryService } from './datasource-factory.service';

describe('DatasourceFactoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DatasourceFactoryService = TestBed.get(DatasourceFactoryService);
    expect(service).toBeTruthy();
  });
});
