namespace ExpenseManager.Server.Models
{
    public class ExpenseCategory
    {
        public int ExpenseId { get; set; }
        public Expense Expense { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
