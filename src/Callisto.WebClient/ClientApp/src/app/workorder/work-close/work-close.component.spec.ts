import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkCloseComponent } from './work-close.component';

describe('WorkCloseComponent', () => {
  let component: WorkCloseComponent;
  let fixture: ComponentFixture<WorkCloseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkCloseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkCloseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
