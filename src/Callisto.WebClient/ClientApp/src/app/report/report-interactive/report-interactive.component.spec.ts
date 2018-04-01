import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportInteractiveComponent } from './report-interactive.component';

describe('ReportInteractiveComponent', () => {
  let component: ReportInteractiveComponent;
  let fixture: ComponentFixture<ReportInteractiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportInteractiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportInteractiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
