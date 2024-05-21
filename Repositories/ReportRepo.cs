using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
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

        internal async Task<int> TotalReport()
        {
                return await dbContext.Reports.Where(r => r.status == "Ok").CountAsync();
 
        }

        internal async Task<List<chart_total_receiving_report>> getChart()
        {
            var query = from r in dbContext.Reports
                        join p in dbContext.Posts on r.post_id equals p.post_id
                        group r by new { p.user.company.company_name } into g
                        select new chart_total_receiving_report
                        {
                            total_report = g.Count(),
                            company_name = g.Key.company_name,
                        };
            var top3report = await query
                            .OrderByDescending(x => x.total_report)  // Order by total rating in descending order
                            .Take(3)  // Take the top 3 results
                            .ToListAsync();
            return top3report;
        }

    }
}
