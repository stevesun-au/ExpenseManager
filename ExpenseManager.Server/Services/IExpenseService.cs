using ExpenseManager.Server.DTOs;
using ExpenseManager.Server.Models;

namespace ExpenseManager.Server.Services
{
    public interface IExpenseService
    {
        Task<List<ExpenseDto>> GetAllExpensesAsync();
        Task<Expense> CreateExpenseAsync(CreateExpenseDto expenseDto);
    }
}
