import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportQnaComponent } from './report-qna.component';

describe('ReportQnaComponent', () => {
  let component: ReportQnaComponent;
  let fixture: ComponentFixture<ReportQnaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportQnaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportQnaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
