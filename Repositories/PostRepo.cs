using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
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
            return await dbcontext.Posts.Where(p => p.status !="reported"&& p.status!="deleted").Include(p=>p.user).Include(p=>p.user.company).ToListAsync();
        }
        public async Task<List<Post>> SearchAllPostForAdmin()
        {
            return await dbcontext.Posts.Where(p => p.status != "reported" && p.status != "deleted").Include(p => p.user).Include(p => p.user.company).ToListAsync();
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
            return await dbcontext.Posts.Where(p=>p.post_id==post_id && p.status!="reported").Include(p=>p.user).FirstOrDefaultAsync();
        }
        public async Task<List<Post>> SearchPost(string search_value)
        {
            return await dbcontext.Posts.Where(p=>p.post_title.Contains(search_value)).ToListAsync();
        }
        public async Task<List<Post>> SearchAllPostByUserId(int user_id)
        {
            return await dbcontext.Posts.Where(p=>(p.user_id==user_id) && (p.status!="reported" && p.status!="deleted")).Include(u=>u.user).Include(c=>c.user.company).ToListAsync();
        }
        internal async Task<List<Post>> SearchAllPostWithCondition(string search_value, string condition)
        {
            if(condition =="Company_name")
            {
                return await dbcontext.Posts.Where(p=>p.user.company.company_name.Contains(search_value) && p.status != "reported" && p.status != "deleted").Include(p => p.user).Include(p => p.user.company).ToListAsync();
            }    
            else if (condition=="Position")
            {
                return await dbcontext.Posts.Where(p => p.Position.Contains(search_value) && p.status != "reported" && p.status != "deleted").Include(p => p.user).Include(p => p.user.company).ToListAsync();
            }
            else
            {
               
                return await dbcontext.Posts.Where(p=>p.address.Contains(search_value) && p.status != "reported" && p.status != "deleted").Include(p => p.user).Include(p => p.user.company).ToListAsync();
            }
        }

        internal async Task<int> TotalPost()
        {
            return await dbcontext.Posts.Where(p=>p.status!="reported" && p.status!="deleted").CountAsync();
        }

        internal async Task<List<chart_total_receving_candidate>> getChart()
        {
            var query = from p in dbcontext.Posts
                        join al in dbcontext.Appliedjobs on p.post_id equals al.post_id
                        group al by new { p.user.company.company_name } into g
                        select new chart_total_receving_candidate
                        {
                            total_candidates = g.Count(),
                            company_name = g.Key.company_name,
                        };
            var top3Candidates = await query
                            .OrderByDescending(x => x.total_candidates)  // Order by total rating in descending order
                            .Take(3)  // Take the top 3 results
                            .ToListAsync();
            return top3Candidates;
        }
    }
}
