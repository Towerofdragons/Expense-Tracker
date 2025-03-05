using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string ReportType { get; set; } // Monthly, Yearly

        [Required]
        public DateTime GeneratedAt { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
