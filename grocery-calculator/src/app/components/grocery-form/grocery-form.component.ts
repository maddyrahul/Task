import { Component, OnInit } from '@angular/core';
import { GroceryService } from '../../services/grocery.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-grocery-form',
  templateUrl: './grocery-form.component.html',
  styleUrl: './grocery-form.component.css'
})
export class GroceryFormComponent implements OnInit {
  groceryForm!: FormGroup;
  showPopUp: boolean = false; // Controls the visibility of the pop-up message

  constructor(private fb: FormBuilder, private groceryService: GroceryService) {}

  ngOnInit(): void {
    this.groceryForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      price: [
        null,
        [Validators.required, Validators.min(0.01), Validators.pattern(/^\d+(\.\d{1,2})?$/)],
      ],
      quantity: [null, [Validators.required, Validators.min(1), Validators.max(100)]],
    });
  }

  addItem(): void {
    if (this.groceryForm.invalid) {
      this.groceryForm.markAllAsTouched(); 
      return;
    }

    const item = this.groceryForm.value;
    this.groceryService.addItem(item);
    this.groceryForm.reset(); // Clear the form after successful addition

    // Show the pop-up message
    this.showPopUp = true;

    // Hide the pop-up message after 2 seconds
    setTimeout(() => {
      this.showPopUp = false;
    }, 2000);
  }

  get formControls() {
    return this.groceryForm.controls;
  }
}
