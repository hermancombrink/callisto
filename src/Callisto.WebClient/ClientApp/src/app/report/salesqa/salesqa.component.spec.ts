import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesqaComponent } from './salesqa.component';

describe('SalesqaComponent', () => {
  let component: SalesqaComponent;
  let fixture: ComponentFixture<SalesqaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalesqaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesqaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
