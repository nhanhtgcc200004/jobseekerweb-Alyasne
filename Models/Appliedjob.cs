using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Appliedjob
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int appliedjob_id {  get; set; }
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        public int post_id { get; set; }
        [ForeignKey("post_id")]
        public Post post { get; set; }
        public string status {  get; set; }

       
    }
}
