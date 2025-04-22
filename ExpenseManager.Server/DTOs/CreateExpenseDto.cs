using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Server.DTOs
{
    public class CreateExpenseDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
        public decimal Amount { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one category must be selected")]
        public List<int> CategoryIds { get; set; } = new List<int>();

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
