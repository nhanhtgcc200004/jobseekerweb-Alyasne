using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Reply_Comment
    {
        [Key]
        [Required(ErrorMessage = "Reply_id can't be empty")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reply_id { get; set; }
        public int user_id {  get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        public string reply_content {  get; set; }
        public int comment_id {  get; set; }
        [ForeignKey("comment_id")]
        public Comment Comment { get; set; }
        public DateTime date_reply {  get; set; } 
    }
}
