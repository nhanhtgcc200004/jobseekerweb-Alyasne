using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class Company
    {
        public Company() { }

        public Company(int conpany_id, string conpany_name, string email_conpany, string position, string user_Name, string address, string status)
        {
            this.conpany_id = conpany_id;
            this.company_name = conpany_name;
            Email_conpany = email_conpany;
            this.position = position;
            Address = address;
            this.status = status;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int conpany_id {  get; set; }
        [Required]
        public string company_name {  get; set; }
        [Required]
        public string Email_conpany {  get; set; }
        [Required]
        public string position {  get; set; }
        [Required]
        public string Address {  get; set; }
        public string status {  get; set; }
    }
}
