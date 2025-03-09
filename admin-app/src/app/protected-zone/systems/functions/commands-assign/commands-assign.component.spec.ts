import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandsAssignComponent } from './commands-assign.component';

describe('CommandsAssignComponent', () => {
  let component: CommandsAssignComponent;
  let fixture: ComponentFixture<CommandsAssignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandsAssignComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandsAssignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
