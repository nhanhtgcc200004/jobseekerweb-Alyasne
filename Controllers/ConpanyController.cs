using EnterpriceWeb.Controllers;
using finalyearproject.Models;
using finalyearproject.Repositories;
using finalyearproject.SubSystem.Mailutils;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class ConpanyController : Controller
    {
        private ISession session;
        private ConpanyRepo _conpanyRepo;
        private ApplicationDBcontext _dbcontext;
        private CvRepo cvRepo;
        private int user_id;
        private string role;
        public ConpanyController(ApplicationDBcontext dBcontext, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = dBcontext;
            _conpanyRepo = new ConpanyRepo(_dbcontext);
            session = httpContextAccessor.HttpContext.Session;
            user_id = (int)session.GetInt32("user_id");
            role = session.GetString("role");
            
        }
        public async Task<IActionResult> ProfileConpany(int conpany_id)
        {
            Conpany conpany= await _conpanyRepo.SearchConpanyById(conpany_id);
            return View(conpany);
        }
    }
}
