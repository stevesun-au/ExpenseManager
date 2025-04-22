import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ExpenseService } from '../../services/expense.service';
import { Expense } from '../../models/expense.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-expense-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatIconModule,
  ],
  templateUrl: './expense-form.component.html',
  styleUrl: './expense-form.component.css'
})
export class ExpenseFormComponent {
  allErrors: string = ''; 
  expenseForm: FormGroup;
  people = [{ id: 1, name: 'Anton' }, { id: 2, name: 'Steve' }];
  categories = [{ id: 1, name: 'Office' }, { id: 2, name: 'Home' }];

  constructor(private fb: FormBuilder,
    private expenseService: ExpenseService,
    private http: HttpClient,
    private router: Router) {

    this.expenseForm = this.fb.group({
      description: ['', Validators.required],
      amount: ['', [Validators.required, Validators.min(0.01)]],
      personId: ['', Validators.required],
      categoryIds: [[], [Validators.required, Validators.minLength(1)]],
      date: [new Date(), Validators.required]
    });
  }
  onSubmit() {
    if (this.expenseForm.valid) {
      const formData: Expense = this.expenseForm.value; // Cast to Expense type

      // Use ExpenseService to send data
      this.expenseService.addExpense(formData).subscribe({
        next: (response: any) => {
          console.log('Success:', response);
          alert('Expense saved successfully!');
          this.expenseForm.reset();
          this.router.navigate(['/add-expense']);
        },
        error: (err: any) => {
          // Check if the error is a 400 (Bad Request) and has validation errors
          if (err.status === 400 && err.error?.errors) {
            const validationErrors = err.error.errors;

            // Collect all error messages
            this.allErrors = '';
            for (const fieldName in validationErrors) {
              if (validationErrors.hasOwnProperty(fieldName)) {
                this.allErrors += validationErrors[fieldName].join(', ') + '\n'; // Join multiple error messages with a line break
              }
            }
            
          } else {
            alert('Failed to save expense. Please try again.');
          }
        }
      });
    }
  }

  formatDecimal(controlName: string) {
    const control = this.expenseForm.get(controlName);
    const value = parseFloat(control?.value);

    if (!isNaN(value)) {
      control?.setValue(value.toFixed(2)); 
    }
  }

}
