using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-U7B3IJO\\SQLEXPRESS;Database=ExpenseTrackerDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascade delete issue on Budgets table
            modelBuilder.Entity<Budget>()
                .HasOne(b => b.User)  // Budget has one User
                .WithMany(u => u.Budgets) // User has many Budgets
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents cascade delete

        }
    }
}

