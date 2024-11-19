import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';

@Injectable({
  providedIn: 'root'
})
export class GroceryService {
  private items = new BehaviorSubject<any[]>(this.getItemsFromLocalStorage());
  items$ = this.items.asObservable();

  addItem(item: any): void {
    const currentItems = this.getItemsFromLocalStorage();
    currentItems.push(item);
    this.saveItemsToLocalStorage(currentItems);
    this.items.next(currentItems); // Notify subscribers
  }

  clearItems(): void {
    localStorage.removeItem('groceryItems');
    this.items.next([]); // Notify subscribers
  }

  private getItemsFromLocalStorage(): any[] {
    const data = localStorage.getItem('groceryItems');
    return data ? JSON.parse(data) : [];
  }

  private saveItemsToLocalStorage(items: any[]): void {
    localStorage.setItem('groceryItems', JSON.stringify(items));
  }
}
