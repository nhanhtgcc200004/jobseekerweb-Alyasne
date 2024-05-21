using finalyearproject.Models;
using finalyearproject.Repositories;
using finalyearproject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDBcontext _dbcontext;
        private PostRepo PostRepo;
        private ISession session;
        private int user_id;
        private string role;
        public SearchController(ApplicationDBcontext dbcontext, IHttpContextAccessor httpContextAccessor)
        {
            _dbcontext = dbcontext;
            PostRepo = new PostRepo(_dbcontext);
            session = httpContextAccessor.HttpContext.Session;
            user_id =(int) session.GetInt32("user_id");
            role = session.GetString("role");
        }
        public IActionResult Index() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Result(string search_value,string condition)
        {
            if (ChecktypeSearch())
            {
                TempData["user_id"] = user_id;
                TempData["name"] = session.GetString("name");
                TempData["avatar"] = session.GetString("avatar");
                TempData["role"] = session.GetString("role");
                if (role == "user")
                {
                    List<Post> posts = await HandleSearchFollowCondition(search_value, condition);
                    TempData["Layout"] = "UserLayout";
                    return PartialView("~/Views/Post/Components/Post_search.cshtml", posts);
                }
                else if(role =="Recruiter")
                {
                    List<Post> posts = await HandleSearchFollowCondition(search_value, condition);
                    TempData["Layout"] = "RecruiterLayout";
                    return PartialView("~/Views/Post/Components/Post_search.cshtml", posts);
                }
                else
                {
                    List<Post> posts = await HandleSearchFollowCondition(search_value, condition);
                    TempData["Layout"] = "AdminLayout";
                    return PartialView("~/Views/Post/Components/Post_search.cshtml", posts);
                }
            }
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> PostManagement(string search_value, string condition)
        {
            if (ChecktypeSearch())
            {
                TempData["user_id"] = user_id;
                TempData["name"] = session.GetString("name");
                TempData["avatar"] = session.GetString("avatar");
                TempData["role"] = session.GetString("role");
               
                if (role == "Recruiter")
                {
                    List<Post> posts = await HandleSearchFollowCondition(search_value, condition);
                    TempData["Layout"] = "RecruiterLayout";
                    return PartialView("~/Views/Post/Components/Post_search_management.cshtml", posts);
                }
            }
            return NotFound();

        }

        private bool ChecktypeSearch()
        {
            return true;
        }

        private async Task<List<Post>> HandleSearchFollowCondition(string search_value, string condition)
        {
            List<Post> posts =  await PostRepo.SearchAllPostWithCondition(search_value, condition);
            return posts;
        }

    }
}
