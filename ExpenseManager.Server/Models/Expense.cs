using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Server.Models
{

    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public ICollection<ExpenseCategory> ExpenseCategories { get; set; }
    }
}
