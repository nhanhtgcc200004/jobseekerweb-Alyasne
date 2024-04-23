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
        public async Task<List<Post>> SearchAllPost()
        {
            return await dbcontext.Posts.Where(p => p.status !="reported").ToListAsync();
        }
        public async Task<Post> SearchPostById(int post_id)
        {
            return await dbcontext.Posts.Where(p=>p.post_id==post_id && p.status!="reported").FirstOrDefaultAsync();
        }
        public async Task<List<Post>> SearchPost(string search_value)
        {
            return await dbcontext.Posts.Where(p=>p.post_title.Contains(search_value)).ToListAsync();
        }

        internal List<Post> SearchAllPostWithCondition(string search_value, string condition)
        {
            throw new NotImplementedException();
        }
    }
}
