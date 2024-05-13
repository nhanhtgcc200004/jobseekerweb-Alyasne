using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class ReportRepo
    {
        private ApplicationDBcontext dbContext;
        public ReportRepo(ApplicationDBcontext dbcontext)
        {
           this.dbContext=dbcontext;
        }
        public async Task<List<Report>> SearchAllReport()
        {
            return await dbContext.Reports.Where(r=>r.status== "Processing..").Include(R=>R.reciver).Include(R=>R.repoter).ToListAsync();
        }
        public async Task<Report> SearchReportById(int report_id)
        {
            return await dbContext.Reports.Where(r=>r.report_id==report_id).FirstAsync();
        }
    }
}
