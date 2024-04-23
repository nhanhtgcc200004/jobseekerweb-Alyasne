using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Review
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int review_id { get; set; }
        public int reviewer_id {  get; set; }
        [ForeignKey("reviewer_id")]
        public User User { get; set; }
        [Required]
        public string review_content {  get; set; }
        [Required]
        public int stars {  get; set; }
        [Required]
        public string status {  get; set; }
    }
}
