using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        [Required]
        public string QuestionType { get; set; }

        [Required]
        public int OrderIndex { get; set; }

        public int SurveyID { get; set; }
    }
}