using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class CvRepo
    {
        private ApplicationDBcontext _dbcontext;
        public CvRepo(ApplicationDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        }
        public async Task<CV> SearchCvOfUser(int user_id)
        {
            return await _dbcontext.CVs.Where(c=>c.user_id==user_id).Include(u=>u.user).FirstOrDefaultAsync();
        }
    }
}
