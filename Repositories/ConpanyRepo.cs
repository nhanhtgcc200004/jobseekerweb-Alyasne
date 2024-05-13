﻿using finalyearproject.Models;
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
        public async Task<List<Company>> SearchAllConpany()
        {
            return await _dbcontext.Companys.ToListAsync();
        }
        public async Task<Company> SearchConpanyById(int conpany_id)
        {
            return await _dbcontext.Companys.Where(c=>c.conpany_id==conpany_id).FirstOrDefaultAsync();
        }
        
    }
}
