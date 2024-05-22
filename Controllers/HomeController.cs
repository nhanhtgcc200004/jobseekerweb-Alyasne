using finalyearproject.Models;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Drawing.Text;

namespace finalyearproject.Controllers
{
    public class HomeController : Controller
    {
        private PostRepo postRepo;
        private readonly ApplicationDBcontext _dbContext;
        private ISession Session;
        private int user_id;
        private string role;
        public HomeController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            postRepo = new PostRepo(_dbContext);
            Session = httpContextAccessor.HttpContext.Session;
            user_id = (int)Session.GetInt32("user_id");
            role = Session.GetString("role");
        }

        public async Task<IActionResult> Recruiter()
        {
            List<Post> posts = await postRepo.SearchAllPostForAdmin();
            TempData["user_id"] = user_id;
            TempData["role"] = role;
            TempData["avatar"] = Session.GetString("avatar");
            TempData["name"] = Session.GetString("name");
            return View(posts);
        }
        
        public async Task<IActionResult> Candidate()
        {
            List<Post> posts = await postRepo.SearchAllPostForAdmin();
            TempData["user_id"] = user_id;
            TempData["role"] = role;
            TempData["avatar"] = Session.GetString("avatar");
            TempData["name"] = Session.GetString("name");
            return View(posts);
        }
        
    }
}