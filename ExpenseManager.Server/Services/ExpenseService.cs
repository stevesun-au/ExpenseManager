using ExpenseManager.Server.Data;
using ExpenseManager.Server.DTOs;
using ExpenseManager.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Server.Services
{
    public class ExpenseService : IExpenseService
    {

        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenseDto>> GetAllExpensesAsync()
        {
            return await _context.Expenses
                .Include(e => e.Person)
                .Include(e => e.ExpenseCategories)
                    .ThenInclude(ec => ec.Category)
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    Date = e.Date,
                    PersonName = e.Person.Name,
                    Categories = e.ExpenseCategories.Select(ec => ec.Category.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<Expense> CreateExpenseAsync(CreateExpenseDto expenseDto)
        {
            var expense = new Expense
            {
                Description = expenseDto.Description,
                Amount = expenseDto.Amount,
                PersonId = expenseDto.PersonId,
                ExpenseCategories = expenseDto.CategoryIds.Select(id => new ExpenseCategory
                {
                    CategoryId = id
                }).ToList()
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return expense;
        }
    }
}
