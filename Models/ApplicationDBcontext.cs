using Microsoft.EntityFrameworkCore;
using System.Data;

namespace finalyearproject.Models
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext() { }
        public ApplicationDBcontext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<Appliedjob> Appliedjobs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Company>().HasData(new Company(999,"a","b","c","open"));
            modelBuilder.Entity<User>().HasData(new User(1, "a", "abc@gmail.com", "123456", "user","nhan","Male","07777","13/05/2002","Ok",999));
        }
       
    }
}
