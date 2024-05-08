using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class PostRepo
    {
        private ApplicationDBcontext dbcontext;

        public PostRepo(ApplicationDBcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<List<Post>> SearchAllPostForHome()
        {
            return await dbcontext.Posts.Where(p => p.status !="reported"&& p.status!="deleted").ToListAsync();
        }
        public async Task<List<Post>> SearchAllPostForManagement()
        {
            return await dbcontext.Posts.ToListAsync();
        }
        public async Task<List<Post>> SearchAllUserPost(int conpany_id)
        {
            return await dbcontext.Posts.Where(p=>p.post_id==conpany_id).ToListAsync();
        }
        public async Task<Post> SearchPostById(int post_id)
        {
            return await dbcontext.Posts.Where(p=>p.post_id==post_id && p.status!="reported").FirstOrDefaultAsync();
        }
        public async Task<List<Post>> SearchPost(string search_value)
        {
            return await dbcontext.Posts.Where(p=>p.post_title.Contains(search_value)).ToListAsync();
        }
        public async Task<List<Post>> SearchAllPostByUserId(int user_id)
        {
            return await dbcontext.Posts.Where(p=>p.user_id==user_id && p.status!="reported" || p.status!="deleted").Include(u=>u.user).Include(c=>c.user.conpany).ToListAsync();
        }
        internal List<Post> SearchAllPostWithCondition(string search_value, string condition)
        {
            throw new NotImplementedException();
        }
    }
}
