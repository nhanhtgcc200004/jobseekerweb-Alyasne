using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Comment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int comment_id {  get; set; }
        [Required(ErrorMessage ="comment content can't be empty")]
        public string comment_content {  get; set; }
        public int rating {  get; set; }
        public int user_id {  get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        public int post_id {  get; set; }
        [ForeignKey("post_id")]
        public Post post { get; set; }
        public DateTime date_comment { get; set; }
    }
}
