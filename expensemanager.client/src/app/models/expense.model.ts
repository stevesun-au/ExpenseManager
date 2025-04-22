export interface Expense {
  id?: number;
  description: string;
  amount: number;
  date: Date;
  personId: number;
  categoryIds: number[];
}
