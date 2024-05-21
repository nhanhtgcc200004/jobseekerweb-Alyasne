using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class UserRepo
    {
        private ApplicationDBcontext dbcontext;
        public UserRepo(ApplicationDBcontext dBcontext)
        {
           this.dbcontext = dBcontext;
        }
        public async Task<User> SearchUserById(int id)
        {
           User user = await dbcontext.Users.Where(u => u.user_id == id).Include(u=>u.company).FirstAsync();
            return user;
        }
        public async Task<User> Login(string email,string password)
        {
            User user= await dbcontext.Users.Where(u=>u.Email==email &&  u.Password==password).FirstOrDefaultAsync();
            return user;
        }
        public async Task<List<User>> SearchAllUser()
        {
            return await dbcontext.Users.Where(u=>u.Status!="banned").Include(u=>u.company).ToListAsync();
        }
        public async Task<List<User>> SearchAllWorker()
        {
            return await dbcontext.Users.Where(u => u.Status != "0" && u.role=="user").ToListAsync();
        }
        
        public async Task<User> SearchUserByMail(string email)
        {
            User user = await dbcontext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> SearchUserJustInsert()
        {
            return await dbcontext.Users.LastAsync();
        }
        public async Task<User> Register(string Email)
        {
            User user = await dbcontext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            return user;
        }

        internal async Task<int> TotalUser()
        {
            return await dbcontext.Users.Where(u => u.Status == "Ok" && u.role=="user").CountAsync();
        }
        internal async Task<int> TotalRecruiter()
        {
            return await dbcontext.Users.Where(u => u.Status == "Ok" && u.role == "Recruiter").CountAsync();
        }
    }
}
