import { Component, OnInit } from '@angular/core';
import { GroceryService } from '../../services/grocery.service';

@Component({
  selector: 'app-grocery-summary',
  templateUrl: './grocery-summary.component.html',
  styleUrl: './grocery-summary.component.css'
})
export class GrocerySummaryComponent implements OnInit {
  items: any[] = [];
  totalCost: number = 0;

  constructor(private groceryService: GroceryService) {}

  ngOnInit(): void {
    this.groceryService.items$.subscribe((items) => {
      this.items = items;
      this.totalCost = this.calculateTotalCost();
    });
  }

  clearAll(): void {
    this.groceryService.clearItems();
  }

  private calculateTotalCost(): number {
    return this.items.reduce((sum, item) => sum + item.price * item.quantity, 0);
  }
}