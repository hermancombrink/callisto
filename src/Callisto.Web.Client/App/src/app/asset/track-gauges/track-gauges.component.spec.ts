import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackGaugesComponent } from './track-gauges.component';

describe('TrackGaugesComponent', () => {
  let component: TrackGaugesComponent;
  let fixture: ComponentFixture<TrackGaugesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrackGaugesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrackGaugesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
