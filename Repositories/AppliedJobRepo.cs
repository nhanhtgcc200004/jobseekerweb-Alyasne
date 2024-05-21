using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class AppliedJobRepo
    {
        private ApplicationDBcontext _dbcontext;
        public AppliedJobRepo(ApplicationDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        }
        public async Task<List<Appliedjob>> SearchAllCandidateOfPost(int post_id)
        {
            return await _dbcontext.Appliedjobs.Where(al=>al.post_id == post_id).Include(al=>al.post).Include(al=>al.post.user).Include(al=>al.user).ToListAsync();
        }
        public async Task<Appliedjob> SearchAppliedById(int applied_id)
        {
            return await _dbcontext.Appliedjobs.Where(al => al.appliedjob_id == applied_id).Include(al => al.post).Include(al=>al.user).FirstAsync();
        }

        internal async Task<List<Appliedjob>> SearchAllAppliedByUserId(int user_id)
        {
            return await _dbcontext.Appliedjobs.Where(al => al.user_id == user_id).Include(al => al.user).Include(al => al.post).ToListAsync();
        }
    }
}
