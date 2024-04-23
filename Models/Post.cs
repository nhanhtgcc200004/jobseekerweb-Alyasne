using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Post
    {
        [Key]
        [Required(ErrorMessage ="Post_id can't be empty")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int post_id {  get; set; }
        public int user_id {  get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        [Required(ErrorMessage ="post title can't be empty")]
        public string post_title { get; set;}
        [Required(ErrorMessage = "post body can't be empty")]
        public string post_body {  get; set;}
        public string location_work {get; set;}
        public string date_post { get; set;}
        public string status { get; set;}
    }
}
