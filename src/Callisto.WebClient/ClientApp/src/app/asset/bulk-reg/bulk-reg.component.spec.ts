import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkRegComponent } from './bulk-reg.component';

describe('BulkRegComponent', () => {
  let component: BulkRegComponent;
  let fixture: ComponentFixture<BulkRegComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BulkRegComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkRegComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
