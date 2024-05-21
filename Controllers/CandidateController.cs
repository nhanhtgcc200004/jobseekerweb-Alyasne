using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Runtime.Intrinsics.Arm;

namespace finalyearproject.Controllers
{
    public class CandidateController : Controller
    {
        private AppliedJobRepo appliedjobRepo;
        private PostRepo postRepo;
        private CommentRepo commentRepo;
       private ISession session;
        private readonly ApplicationDBcontext _dbContext;
        private int user_id;
        private string role;
        public CandidateController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            postRepo = new PostRepo(_dbContext);
            appliedjobRepo=new AppliedJobRepo(_dbContext);
            commentRepo=new CommentRepo(_dbContext);
            session = httpContextAccessor.HttpContext.Session;
            user_id = (int)session.GetInt32("user_id");
            role = session.GetString("role");

        }
        public async Task<IActionResult> Index(int post_id)
        {
            List<Appliedjob> jobs = await appliedjobRepo.SearchAllCandidateOfPost(post_id);
            TempData["role"] = role;
            TempData["avatar"] = session.GetString("avatar");
            TempData["name"] = session.GetString("name");
            return View(jobs);
        }

        public async Task<IActionResult> Appliedjobmanagement()
        {
            TempData["user_id"] = user_id;
            TempData["name"] = session.GetString("name");
            TempData["avatar"] = session.GetString("avatar");
            List<Appliedjob> appliedjobs = await appliedjobRepo.SearchAllAppliedByUserId(user_id);
            return View(appliedjobs);
        }

        [HttpPost]
        public async Task AcceptApplied(int appliedjob_id)
        {
            if (CheckInfor())
            {
                await HandleApplied(appliedjob_id);
            }
            
        }
        [HttpPost]
        public async Task RefuseApplied(int appliedjob_id)
        {
            if (CheckInfor())
            {
              await HandleRefuse(appliedjob_id);
            }

        }

        public async Task<IActionResult> Detail(int post_id)
        {
            
            List<Comment> comments = await commentRepo.GetAllCommentByPostId(post_id);
            if (CheckInfor())
            {
                Post post = await postRepo.SearchPostById(post_id);
                Post_CommentViewModel post_comment = new Post_CommentViewModel(post, comments);
                TempData["role"] = role;
                TempData["user_id"] = user_id;
                TempData["name"] = session.GetString("name");
                TempData["avatar"] = session.GetString("avatar");
                return View(post_comment);
            }
            else return NotFound();
        }
        private async Task HandleRefuse(int id)
        {
            Appliedjob appliedjob = await appliedjobRepo.SearchAppliedById(id);
            if(appliedjob.status!="Refuse")
                appliedjob.post.total_of_candidates--;
            appliedjob.status = "Refuse";
           
            _dbContext.Update(appliedjob);
            await _dbContext.SaveChangesAsync();
        }
        private async Task  HandleApplied(int id)
        {
            Appliedjob appliedjob = await appliedjobRepo.SearchAppliedById(id);
            appliedjob.status = "Accept";
            _dbContext.Update(appliedjob);
           await _dbContext.SaveChangesAsync();
           
        }
        private bool CheckInfor()
        {
            if (user_id != null)
            {
                return true;
            }
            return false;
        }
    }
}
