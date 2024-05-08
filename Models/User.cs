using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalyearproject.Models
{
    public class User
    {
        public User() { }

        public User(int user_id, string avatar, string email, string password, string role, string name, string gender, string phone, string birthday, string status, string viewable, int conpany_id)
        {
            this.user_id = user_id;
            this.avatar = avatar;
            Email = email;
            Password = password;
            this.role = role;
            Name = name;
            Gender = gender;
            Phone = phone;
            Birthday = birthday;
            Status = status;
            Viewable = viewable;
            this.conpany_id = conpany_id;
            this.conpany = conpany;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        public string avatar {  get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string role {  get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }
        public string Status { get; set; }
        public string Viewable {  get; set; }
        public int conpany_id { get; set; }
        [ForeignKey("conpany_id")]
        public Conpany conpany { get; set; }

    }
}
