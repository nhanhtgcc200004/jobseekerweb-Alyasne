using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Verification
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string verify_id { get; set; }
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        [Required]
        public string verify_code {  get; set; }

    }
}
