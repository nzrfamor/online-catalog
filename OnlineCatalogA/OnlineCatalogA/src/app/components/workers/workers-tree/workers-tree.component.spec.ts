import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkersTreeComponent } from './workers-tree.component';

describe('WorkersTreeComponent', () => {
  let component: WorkersTreeComponent;
  let fixture: ComponentFixture<WorkersTreeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkersTreeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkersTreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
