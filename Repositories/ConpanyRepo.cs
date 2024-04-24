using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class ConpanyRepo
    {
        private ApplicationDBcontext _dbcontext;
        public ConpanyRepo(ApplicationDBcontext applicationDBcontext)
        {
            _dbcontext = applicationDBcontext;
        }
        public async Task<List<Conpany>> SearchAllConpany()
        {
            return await _dbcontext.Conpanys.ToListAsync();
        }
        public async Task<Conpany> SearchConpanyById(int conpany_id)
        {
            return await _dbcontext.Conpanys.Where(c=>c.company_id==conpany_id).FirstOrDefaultAsync();
        }
        
    }
}
