using finalyearproject.Models;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class CandidateController : Controller
    {
        private AppliedJobRepo appliedjobRepo;
        private PostRepo postRepo;
        private readonly ApplicationDBcontext _dbContext;
        private int user_id;
        private int role;
        public CandidateController(ApplicationDBcontext dbContext)
        {
            _dbContext = dbContext;
            postRepo = new PostRepo(_dbContext);
            appliedjobRepo=new AppliedJobRepo(_dbContext);
        }
        public async Task<IActionResult> Index(int post_id)
        {
            List<Appliedjob> jobs = await appliedjobRepo.SearchAllCandidateOfPost(post_id);
            return View(jobs);
        }

        public async Task<IActionResult> Appliedjobmanagement()
        {
            List<Appliedjob> appliedjobs = await appliedjobRepo.SearchAllAppliedByUserId(user_id);
            return View(appliedjobs);
        }

        [HttpPost]
        public async void AcceptApplied(int appliedjob_id)
        {
            if (CheckInfor())
            {
                HandleApplied(appliedjob_id);
            }
            
        }
        [HttpPost]
        public async void RefuseApplied(int appliedjob_id)
        {
            if (CheckInfor())
            {
                HandleRefuse(appliedjob_id);
            }

        }

        private async void HandleRefuse(int id)
        {
            Appliedjob appliedjob = await appliedjobRepo.SearchAppliedById(id);
            appliedjob.status = "Refuse";
            appliedjob.post.total_of_candidates--;
            _dbContext.Update(appliedjob);
            _dbContext.SaveChanges();
        }
        private async void HandleApplied(int id)
        {
            Appliedjob appliedjob = await appliedjobRepo.SearchAppliedById(id);
            if (appliedjob.status == "Refuse") appliedjob.post.total_of_candidates++;
            appliedjob.status = "Accept";
            _dbContext.Update(appliedjob);
            _dbContext.SaveChanges();
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
