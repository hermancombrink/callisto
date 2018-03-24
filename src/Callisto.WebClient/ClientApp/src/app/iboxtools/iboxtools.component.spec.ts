import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IxboxtoolsComponent } from './ixboxtools.component';

describe('IxboxtoolsComponent', () => {
  let component: IxboxtoolsComponent;
  let fixture: ComponentFixture<IxboxtoolsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IxboxtoolsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IxboxtoolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
