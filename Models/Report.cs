using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Report
    {
        [Key]
        [Required]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int report_id { get; set; }
        [Required]
        public int reporter_id { get; set; }
        [ForeignKey("reporter_id")]
        public User repoter { get; set; }
        public int reciver_id { get; set; }
        [ForeignKey("reciver_id")]
        public User reciver { get; set; }
        public int post_id { get; set; }
        [ForeignKey("post_id")]
        public Post post { get; set; }
        [Required(ErrorMessage ="content of report can't be empty")]
        public string content_report { get; set; }
        [Required]
        public DateTime date_submit { get; set; }
        [Required]
        public string status { get; set; }
    }
}