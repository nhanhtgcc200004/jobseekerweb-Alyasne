using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class ReviewRepo
    {
        private ApplicationDBcontext _dbcontext;
        public ReviewRepo(ApplicationDBcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<Review>> GetAllReviews()
        {
            return await _dbcontext.Reviews.ToListAsync();
        }
        public async Task<Review> GetReviewById(int review_id)
        {
            return await _dbcontext.Reviews.Where(rev=>rev.review_id==review_id).FirstAsync();
        }
    }
}
