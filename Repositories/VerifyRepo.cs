using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class VerifyRepo
    {
        private ApplicationDBcontext _dbcontext;
        public VerifyRepo(ApplicationDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        } 
        public async Task<Verification> SearchVerifyCodeOfUser(int user_id)
        {
            return await _dbcontext.Verifications.Where(ver => ver.user_id == user_id).FirstAsync();
        }
    }
}
