import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RolesAssignComponent } from './roles-assign.component';

describe('RolesAssignComponent', () => {
  let component: RolesAssignComponent;
  let fixture: ComponentFixture<RolesAssignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RolesAssignComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RolesAssignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
