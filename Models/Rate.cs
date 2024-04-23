using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Rate
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rate_id { get; set; }
        [Required(ErrorMessage ="content can't empty")]
        public string content { get; set; }
        [Required(ErrorMessage ="Star can't empty")]
        public int star {  get; set; }
        public DateTime time_rate { get; set; }

    }
}
