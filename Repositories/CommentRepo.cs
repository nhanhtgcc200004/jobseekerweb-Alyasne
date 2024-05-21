using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class CommentRepo
    {
        private ApplicationDBcontext _dbcontext;
        public CommentRepo(ApplicationDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        }
        public async Task<List<Comment>> GetAllCommentByPostId(int postId)
        {
            return await _dbcontext.Comments.Where(c => c.post_id == postId).Include(c=>c.user).ToListAsync();
        }
        
        public async Task<Comment> GetCommentById(int comment_id)
        {
            return await _dbcontext.Comments.Where(c => c.comment_id == comment_id).FirstOrDefaultAsync();
        }

        internal async Task<List<Chart_total_rating>> getChart()
        {
            var query = from c in _dbcontext.Comments
                        join p in _dbcontext.Posts on c.post_id equals p.post_id
                        group c by new { p.user.company.company_name } into g
                        select new Chart_total_rating
                        {   avg = g.Count(),
                            company_name = g.Key.company_name,
                            total_rating = g.Sum(x => x.rating)
                        };
            var top3Posts = await query
                            .OrderByDescending(x => x.total_rating)  // Order by total rating in descending order
                            .Take(3)  // Take the top 3 results
                            .ToListAsync();
            return top3Posts;
        }

       
    }
}
