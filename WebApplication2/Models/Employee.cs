using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Employee
    {
        [JsonProperty("id")]
        [Column("id")]
        public int Id { get; set; }

        [JsonProperty("employee_id")]
        [Column("employee_id")]
        public int? EmployeeId { get; set; }

        [JsonProperty("position")]
        [Column("position")]
        public string? Position { get; set; }

        [JsonProperty("hours")]
        [Column("hours")]
        public int? Hours { get; set; }

        [JsonProperty("contact_info")]
        [Column("contact_info")]
        public string? ContactInfo { get; set; }

        [JsonProperty("enterprise_id")]
        [Column("enterprise_id")]
        public int? EnterpriseId { get; set; }
    }
}
