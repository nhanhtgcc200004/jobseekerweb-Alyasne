using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace finalyearproject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDBcontext _dbcontext;
        private ISession session;
        private PostRepo postRepo;
        private UserRepo userRepo;
        private ReportRepo reportRepo;
        private int user_id;
        private string role;
        public AdminController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor) 
        { 
            _dbcontext = dbContext;
            postRepo = new PostRepo(_dbcontext);
            userRepo = new UserRepo(_dbcontext);
            reportRepo = new ReportRepo(_dbcontext);
            session = httpContextAccessor.HttpContext.Session;
            user_id = (int)session.GetInt32("user_id");
            role = session.GetString("role");
        }
        public async Task<IActionResult> Index()
        {
            data_chart data_Chart =await HandleGetDataChart();
            return View(data_Chart);
        }

        private async Task<data_chart> HandleGetDataChart()
        {
            List<Report> reports = await reportRepo.SearchAllReport();
            List<User> users = await userRepo.SearchAllWorker();
            List<Post> posts =await postRepo.SearchAllPost();
            return new data_chart(posts, users,reports);
        }
        public async Task<IActionResult> Chart()
        {
            return View();
        }

    }
}
