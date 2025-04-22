import { Component } from '@angular/core';
import { ExpenseService } from '../../services/expense.service';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { DatePipe, CurrencyPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-expense-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatChipsModule, MatIconModule, DatePipe, CurrencyPipe],
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.css']
})
export class ExpenseListComponent {
  displayedColumns: string[] = ['description', 'amount', 'person', 'date', 'categories'];
  expenses: any[] = [];

  constructor(private expenseService: ExpenseService) {
    this.loadExpenses();
  }

  loadExpenses() {
    this.expenseService.getExpenses().subscribe(data => {
      this.expenses = data;
    });
  }

  getCategoryColor(category: string): string {
    const colors: { [key: string]: string } = {
      'Office Expenses': '#5c6bc0',
      'Home Expenses': '#66bb6a'
    };
    return colors[category] || '#78909c';
  }

  // Add this function to check if index is even
  isEven(index: number): boolean {
    return index % 2 === 0;
  }
}
