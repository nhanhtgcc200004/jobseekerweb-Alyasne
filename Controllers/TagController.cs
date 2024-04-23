using finalyearproject.Models;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDBcontext _dbContext;
        private readonly TagRepo tagRepo;
        private readonly UserRepo userRepo;
        private ISession Session;
        private int user_id;
        private string role;
        public TagController(ApplicationDBcontext dbContext, HttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            tagRepo = new TagRepo(_dbContext);
            userRepo = new UserRepo(_dbContext);
            Session = httpContextAccessor.HttpContext.Session;
            user_id = (int)Session.GetInt32("Id");
            role = Session.GetString("role");
        }
        public IActionResult Index()
        {
            if (CheckUser())
                return View();
            return NotFound();
        }
        public IActionResult CreateTag() { return View(); }
        [HttpPost]
        public IActionResult CreateTag([FromForm] Tag tag)
        {
            if (CheckUser())
            {
                CheckTagValue(tag);
                HandleCreateTag(tag);
                return View("Index");
            }
            return NotFound();

        }

        public IActionResult UpdateTag()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateTag([FromForm] Tag tag)
        {
            if (CheckUser())
                CheckTagValue(tag);
            HandleUpdateTag(tag);
            return View();
        }
        public void DeleteTag(int id)
        {
            if (CheckExist(id))
            {
                HandleDeleteTag(new Tag());//(await tagRepo.SearchTagById(id));
            }
            else
            {
                ViewBag.Clear();
                ViewBag.erorr = "The selected tag doesn't exist";
            }

        }

        private bool CheckUser()
        {
            User user = new User();// await userRepo.SearchUserById(user_id);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        private void HandleCreateTag(Tag tag)
        {
            _dbContext.Add(tag);
            _dbContext.SaveChanges();
        }
        private void HandleDeleteTag(Tag tag)
        {
            _dbContext.Remove(tag);
            _dbContext.SaveChanges();
        }

        private bool CheckExist(int id)
        {
            Tag tag = new Tag();// await TagRepo.SearchTagById(id);
            if (tag != null)
            {
                return true;
            }
            return false;
        }

        private void HandleUpdateTag(Tag tag)
        {
            _dbContext.Update(tag);
            _dbContext.SaveChanges();
        }

        private bool CheckTagValue(Tag tag)
        {
            Tag _tag = new Tag();// tagRepo.SearchTagByContent(Tag.Content);
            if (tag == null)
            {
                return true;
            }
            return false;
        }
    }
}
