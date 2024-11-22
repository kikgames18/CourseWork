namespace WebApplication2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Required]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("position")]
        public string Position { get; set; } = string.Empty; // Исправлено

        [Column("hours")]
        public int Hours { get; set; }

        [StringLength(255)]
        [Column("contact_info")]
        public string? ContactInfo { get; set; }

        [Column("enterprise_id")]
        public int EnterpriseId { get; set; }
    }
}
