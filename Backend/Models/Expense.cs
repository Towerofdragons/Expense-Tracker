using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [ForeignKey("User")]
        public string Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User? User { get; set; }
        public Category? Category { get; set; }
    }
}