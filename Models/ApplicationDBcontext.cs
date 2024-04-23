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
        public DbSet<Reply_Comment> Replys { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
       
    }
}
