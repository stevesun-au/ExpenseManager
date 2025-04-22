using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Server.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
