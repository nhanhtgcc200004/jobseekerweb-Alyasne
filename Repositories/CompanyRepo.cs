using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class CompanyRepo
    {
        private ApplicationDBcontext _dbcontext;
        public CompanyRepo(ApplicationDBcontext applicationDBcontext)
        {
            _dbcontext = applicationDBcontext;
        }
        public async Task<List<Company>> SearchAllCompany()
        {
            return await _dbcontext.Companys.ToListAsync();
        }
        
    }
}
