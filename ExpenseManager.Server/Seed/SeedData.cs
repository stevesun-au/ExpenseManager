using ExpenseManager.Server.Data;
using ExpenseManager.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Server.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.People.Any() || context.Categories.Any())
                return; // DB already seeded

            context.People.AddRange(
                new Person { Name = "Anton" },
                new Person { Name = "Steve" }
            );

            context.Categories.AddRange(
                new Category { Name = "Office Expenses" },
                new Category { Name = "Home Expenses" }
            );

            context.SaveChanges();
        }
    }
}
