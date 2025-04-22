using ExpenseManager.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExpenseManager.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for many-to-many
            modelBuilder.Entity<ExpenseCategory>()
                .HasKey(ec => new { ec.ExpenseId, ec.CategoryId });

            // Seed data
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Anton" },
                new Person { Id = 2, Name = "Steve" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Office Expenses" },
                new Category { Id = 2, Name = "Home Expenses" }
            );
        }
    }
}
