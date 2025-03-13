using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [ForeignKey("User")]
        public string Id { get; set; } // Each user has their own categories

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; } // Expense or Income type

        // Navigation properties
        public User? User { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
        public ICollection<Income>? Incomes { get; set; }
        public ICollection<Budget>? Budgets { get; set; }
    }
}
