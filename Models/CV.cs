using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class CV
    {
        [Key]
        [Required(ErrorMessage = "cv_id can't be empty")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cv_id {  get; set; }
        [Required(ErrorMessage ="cv_file can't be empty")]
        public string cv_file {  get; set; }
        [Required(ErrorMessage ="date upload can't be empty")]
        public DateTime date_upload { get; set; }
        
        public int user_id {  get; set; }
        [ForeignKey("user_id")]
        [Required(ErrorMessage ="User can't be empty")]
        public User user { get; set; }

    }
}
