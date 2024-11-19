import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrocerySummaryComponent } from './grocery-summary.component';

describe('GrocerySummaryComponent', () => {
  let component: GrocerySummaryComponent;
  let fixture: ComponentFixture<GrocerySummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GrocerySummaryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GrocerySummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
