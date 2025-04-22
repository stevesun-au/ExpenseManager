namespace ExpenseManager.Server.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PersonName { get; set; }
        public List<string> Categories { get; set; }
    }
}
