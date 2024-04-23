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
           User user = await dbcontext.Users.Where(u => u.user_id == id).FirstAsync();
            return user;
        }
        public async Task<User> Login(string email,string password)
        {
            User user= await dbcontext.Users.Where(u=>u.Email==email &&  u.Password==password).FirstOrDefaultAsync();
            return user;
        }
        public async Task<List<User>> SearchAllUser()
        {
            return await dbcontext.Users.Where(u=>u.Status!="0").ToListAsync();
        }
        public async Task<User> SearchUserByMail(string email)
        {
            User user = await dbcontext.Users.Where(u => u.Email == email).FirstAsync();
            return user;
        }
        public async Task<User> SearchUserJustInsert()
        {
            return await dbcontext.Users.LastAsync();
        }

        
    }
}
