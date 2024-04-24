using finalyearproject.Models;
using finalyearproject.Repositories;
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
        public SearchController(ApplicationDBcontext dbcontext, HttpContextAccessor httpContextAccessor)
        {
            _dbcontext = dbcontext;
            PostRepo = new PostRepo(_dbcontext);
            session = httpContextAccessor.HttpContext.Session;
            user_id =(int) session.GetInt32("user_id");
            role = session.GetString("role");
        }
        public IActionResult Index(string search_value)
        {
            if (ChecktypeSearch())
            {
               return View(HandleSearch(search_value));
            }
            return View();
        }

        private bool ChecktypeSearch()
        {
            return true;
        }

        private async Task<List<Post>> HandleSearch(string search_value)
        {
          return await PostRepo.SearchPost(search_value);
        }

        private async Task<List<Post>> HandleSearchFollowCondition(string search_value, string condition)
        {
            List<Post> posts = PostRepo.SearchAllPostWithCondition(search_value, condition);
            return posts;
        }

    }
}
