using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
/* DbContext is the primary class that works with the db*/
namespace Backend.Models
{
public class TrackerDBContext : IdentityDbContext<User>
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
        //         new User { Id = "1", Name = "Admin", Email = "admin@example.com", Password = "admin"1""2"3", Role = "Admin" },
        //         new User { Id = "2", Name = "John Doe", Email = "john@example.com", Password = "password", Role = "User" }
        //     );

        // modelBuilder.Entity<Income>().HasData(new  { IncomeId = "1",Id = "1", Amount = (decimal)5000.00});
        // modelBuilder.Entity<Expense>().HasData(new { ExpenseId = "1", Id = "2", Amount = (decimal)"2"000.00});

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
        );

       

        // Seed Admin User
        var hasher = new PasswordHasher<User>();
        var admin = new User
        {
            Id = "1001",
            Name = "Admin",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            UserName = "admin@example.com",
            NormalizedUserName = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,
            Role = "Admin",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!"); // Securely hashed password
        modelBuilder.Entity<User>().HasData(admin);

        var testuser = new User
        {
            Id = "1002",
            Name = "John",
            Email = "john@example.com",
            NormalizedEmail = "JOHN@EXAMPLE.COM",
            UserName = "john@example.com",
            NormalizedUserName = "JOHN@EXAMPLE.COM",
            EmailConfirmed = true,
            Role = "user",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        testuser.PasswordHash = hasher.HashPassword(testuser, "Pass123!"); // Securely hashed password
        modelBuilder.Entity<User>().HasData(testuser);

        // Assign Admin User to Role
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "1001", RoleId = "1" },
            new IdentityUserRole<string> { UserId = testuser.Id, RoleId = "2" }
        );

    modelBuilder.Entity<Category>().HasData(
        new { CategoryId = 1, Id = "1002", Name = "Salary", Type = "Income" },
        new { CategoryId = 2, Id = "1002", Name = "Groceries", Type = "Expense" },
        new { CategoryId = 3, Id = "2", Name = "Freelance", Type = "Income" },
        new { CategoryId = 4, Id = "2", Name = "Entertainment", Type = "Expense" }
    );

    modelBuilder.Entity<Income>().HasData(
        new { IncomeId = 1, Id = "1002", CategoryId = 1, Amount = (decimal)5000.00, Description = "Monthly salary", IncomeDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
        new { IncomeId = 2, Id = "2", CategoryId = 3, Amount = (decimal)2000.00, Description = "Freelance project", IncomeDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Expense>().HasData(
        new { ExpenseId = 1, Id = "2", CategoryId = 2, Amount = (decimal)2000.00, Description = "Grocery shopping", ExpenseDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
        new { ExpenseId = 2, Id = "1002", CategoryId = 4, Amount = (decimal)500.00, Description = "Movie night", ExpenseDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Budget>().HasData(
        new { BudgetId = 1, Id = "1002", CategoryId = 2, Amount = (decimal)1000.00, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), CreatedAt = DateTime.UtcNow },
        new { BudgetId = 2, Id = "2", CategoryId = 4, Amount = (decimal)500.00, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Payment>().HasData(
        new { PaymentId = 1, Id = "1002", Amount = (decimal)1000.00, PaymentMethod = "Credit Card", PaymentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
        new { PaymentId = 2, Id = "2", Amount = (decimal)500.00, PaymentMethod = "Cash", PaymentDate = DateTime.UtcNow, CreatedAt = DateTime.UtcNow }
    );

    modelBuilder.Entity<Report>().HasData(
        new { ReportId = 1, Id = "1002", ReportType = "Monthly", GeneratedAt = DateTime.UtcNow },
        new { ReportId = 2, Id = "2", ReportType = "Yearly", GeneratedAt = DateTime.UtcNow }
    );
        
    }
}
}