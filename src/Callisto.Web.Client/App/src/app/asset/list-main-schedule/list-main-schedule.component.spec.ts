import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMainScheduleComponent } from './list-main-schedule.component';

describe('ListMainScheduleComponent', () => {
  let component: ListMainScheduleComponent;
  let fixture: ComponentFixture<ListMainScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListMainScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListMainScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
