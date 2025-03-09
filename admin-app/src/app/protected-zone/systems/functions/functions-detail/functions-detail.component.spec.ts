import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionsDetailComponent } from './functions-detail.component';

describe('FunctionsDetailComponent', () => {
  let component: FunctionsDetailComponent;
  let fixture: ComponentFixture<FunctionsDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FunctionsDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FunctionsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
