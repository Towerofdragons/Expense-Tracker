using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        public bool IsActive { get; set; } = true;

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public override string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User"; //Default role to "User"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Income> Incomes { get; set; } = new List<Income>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
