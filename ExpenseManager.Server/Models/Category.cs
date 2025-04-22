using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Server.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; }
    }

}
