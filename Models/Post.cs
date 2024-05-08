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
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        public string post_title { get; set;}
        [Required(ErrorMessage = "post body can't be empty")]
        public string job_description {  get; set;}
        public string address {  get; set; }
        public string experience {  get; set; }
        public int salary {  get; set; }
        public string Position { get; set;}
        public DateTime date_post { get; set;}
        public string status { get; set;}
        public DateTime expired_date {  get; set;}
        public int limit_candidates {  get; set;}
        public int total_of_candidates {  get; set;}
        public string skill_required {  get; set;}
        public string other_condition {  get; set;}
    }
}
