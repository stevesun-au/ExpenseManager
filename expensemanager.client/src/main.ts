import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter, Route, withComponentInputBinding } from '@angular/router';
import { AppComponent } from './app/app.component';
import { ExpenseFormComponent } from './app/components/expense-form/expense-form.component';
import { ExpenseListComponent } from './app/components/expense-list/expense-list.component';

const routes: Route[] = [
  {
    path: 'add',
    component: ExpenseFormComponent,
    title: 'Add Expense'
  },
  {
    path: '',
    component: ExpenseListComponent,
    title: 'Expense Tracker'
  },
  {
    path: '**',
    redirectTo: ''
  }
];
bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideHttpClient(),
    provideRouter(routes, withComponentInputBinding()),
  ]
}).catch(err => console.error(err));
