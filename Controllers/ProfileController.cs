
using EnterpriceWeb.Controllers;
using finalyearproject.Models;
using finalyearproject.Repositories;
using finalyearproject.SubSystem.Mailutils;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class ProfileController : Controller
    {
        private ISession session;
        private UserRepo _userRepo;
        private ApplicationDBcontext _dbcontext;
        private SendMailSystem _sendMailSystem;
        private CvRepo cvRepo;
        private int user_id;
        private string role;
        public ProfileController(ApplicationDBcontext dBcontext, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = dBcontext;
            _userRepo = new UserRepo(_dbcontext);
            session =httpContextAccessor.HttpContext.Session;
            _sendMailSystem=new SendMailSystem(emailSender, hostEnvironment);
            cvRepo = new CvRepo(_dbcontext);
            user_id =(int) session.GetInt32("user_id");
            role = session.GetString("role");
           
        }
        public async Task<IActionResult> Candidate(int id)
        {
            
            if (Checkinfor(id))
            {
                User user= await _userRepo.SearchUserById(id);
                return View(user);
            }
            return BadRequest();
        }
        public async Task<IActionResult> Conpany(int id)
        {
            User user= await _userRepo.SearchUserById(id);
            if (Checkinfor(user.user_id))
            return View(user);
            return NotFound();
        }
        public async Task<IActionResult> UpdateProfile(int id)
        {
            if (Checkinfor(id))
            {
                User user= await _userRepo.SearchUserById(id);
                
                return View(user);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] User user)
        {
            if (Checkinfor(id))
            {
                HandleUpdateProfile(user);
                return RedirectToAction("Index","Profile",id=id);
            }
            return BadRequest();
        }
        [HttpPost]
        private async Task<IActionResult> downloadUserCv(int user_profile_id)
        { 
            int user_id = (int)session.GetInt32("user_id");
            string role = session.GetString("role");
            if (user_id != null && role != null)
            {
                CV user_cv = await cvRepo.SearchCvOfUser(user_profile_id);
                MemoryStream memory = _sendMailSystem.DownloadSingleFile(user_cv);
                return File(memory.ToArray(), "application/zip", user_cv.user.Name);
            }
            return null;
        }
        private async void UploadCV()
        {

        }
        private void HandleUpdateProfile(User user)
        {
            _dbcontext.Update(user);
            _dbcontext.SaveChanges();
        }

        private bool Checkinfor(int id)
        {
            //if (user_id != null && user_id == id)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }
    }
}
