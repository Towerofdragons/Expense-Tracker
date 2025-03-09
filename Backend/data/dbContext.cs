using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
/* DbContext is the primary class that works with the db*/
namespace Backend.Models
{
public class TrackerDBContext : DbContext
{
  
  public DbSet<User> Users { get; set; }
  public DbSet<Expense> Expense { get; set; }
  public DbSet<Income> Income { get; set; }
  public DbSet<Payment> Payment {get; set; }
  public DbSet<Report> Report {get; set; }
  public DbSet<Category> Categories {get; set; }



  public TrackerDBContext(DbContextOptions<TrackerDBContext> options)
  : base(options)
  {
    
  }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //  modelBuilder.Entity<User>().HasData(
        //         new User { UserId = 1, Name = "Admin", Email = "admin@example.com", Password = "admin123", Role = "Admin" },
        //         new User { UserId = 2, Name = "John Doe", Email = "john@example.com", Password = "password", Role = "User" }
        //     );

        // modelBuilder.Entity<Income>().HasData(new  { IncomeId = 1,UserID = 1, Amount = (decimal)5000.00});
        // modelBuilder.Entity<Expense>().HasData(new { ExpenseId = 1, UserID = 2, Amount = (decimal)2000.00});

      modelBuilder.Entity<User>().HasData(
        new User { UserId = 1, Name = "Admin", Email = "admin@example.com", Password = "admin123", Role = "Admin", CreatedAt = DateTime.UtcNow },
        new User { UserId = 2, Name = "John Doe", Email = "john@example.com", Password = "password", Role = "User", CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Category>().HasData(
        new { CategoryId = 1, UserId = 1, Name = "Salary", Type = "Income" },
        new { CategoryId = 2, UserId = 1, Name = "Groceries", Type = "Expense" },
        new { CategoryId = 3, UserId = 2, Name = "Freelance", Type = "Income" },
        new { CategoryId = 4, UserId = 2, Name = "Entertainment", Type = "Expense" }
    );

    modelBuilder.Entity<Income>().HasData(
        new { IncomeId = 1, UserId = 1, CategoryId = 1, Amount = (decimal)5000.00, Description = "Monthly salary", IncomeDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
        new { IncomeId = 2, UserId = 2, CategoryId = 3, Amount = (decimal)2000.00, Description = "Freelance project", IncomeDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Expense>().HasData(
        new { ExpenseId = 1, UserId = 2, CategoryId = 2, Amount = (decimal)2000.00, Description = "Grocery shopping", ExpenseDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
        new { ExpenseId = 2, UserId = 1, CategoryId = 4, Amount = (decimal)500.00, Description = "Movie night", ExpenseDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Budget>().HasData(
        new { BudgetId = 1, UserId = 1, CategoryId = 2, Amount = (decimal)1000.00, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), CreatedAt = DateTime.UtcNow },
        new { BudgetId = 2, UserId = 2, CategoryId = 4, Amount = (decimal)500.00, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Payment>().HasData(
        new { PaymentId = 1, UserId = 1, Amount = (decimal)1000.00, PaymentMethod = "Credit Card", PaymentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
        new { PaymentId = 2, UserId = 2, Amount = (decimal)500.00, PaymentMethod = "Cash", PaymentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Report>().HasData(
        new { ReportId = 1, UserId = 1, ReportType = "Monthly", GeneratedAt = DateTime.UtcNow },
        new { ReportId = 2, UserId = 2, ReportType = "Yearly", GeneratedAt = DateTime.UtcNow }
    );
        
    }
}
}