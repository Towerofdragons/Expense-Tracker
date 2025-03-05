using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; }
        public Category Category { get; set; }
    }
}
