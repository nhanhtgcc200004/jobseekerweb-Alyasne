using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Conpany
    {
        public Conpany() { }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int company_id {  get; set; }
        [Required]
        public string company_name {  get; set; }
        [Required]
        public string Email_company {  get; set; }
        [Required]
        public string position {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address {  get; set; }
        public string status {  get; set; }
    }
}
