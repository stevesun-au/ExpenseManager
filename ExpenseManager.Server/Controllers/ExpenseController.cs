using ExpenseManager.Server.DTOs;
using ExpenseManager.Server.Models;
using ExpenseManager.Server.Services;
using ExpenseManager.Server.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseDto>>> Get()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            return Ok(expenses);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> Post(CreateExpenseDto expenseDto)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }

            // Manually validate using FluentValidation
            var validator = new ExpenseDtoValidator();
            var validationResult = await validator.ValidateAsync(expenseDto);

            if (!validationResult.IsValid)
            {
                // Log the validation failures for debugging
                foreach (var failure in validationResult.Errors)
                {
                    Console.WriteLine($"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");
                }

                // Return the validation errors in a custom format
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                return BadRequest(new { errors });
            }
            try
            {
                var expense = await _expenseService.CreateExpenseAsync(expenseDto);
                return CreatedAtAction(nameof(Get), new { id = expense.Id }, expense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
