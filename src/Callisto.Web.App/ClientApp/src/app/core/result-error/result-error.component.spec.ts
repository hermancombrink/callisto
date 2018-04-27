import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultErrorComponent } from './result-error.component';

describe('ResultErrorComponent', () => {
  let component: ResultErrorComponent;
  let fixture: ComponentFixture<ResultErrorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResultErrorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
