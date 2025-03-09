import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnowledgeBasesDetailComponent } from './knowledge-bases-detail.component';

describe('KnowledgeBasesDetailComponent', () => {
  let component: KnowledgeBasesDetailComponent;
  let fixture: ComponentFixture<KnowledgeBasesDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KnowledgeBasesDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KnowledgeBasesDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
