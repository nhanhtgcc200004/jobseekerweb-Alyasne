
using EnterpriceWeb.Controllers;
using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
using finalyearproject.Repositories;
using finalyearproject.SubSystem.Mailutils;
using finalyearproject.SubSystem.Support;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

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
            session = httpContextAccessor.HttpContext.Session;
            _sendMailSystem = new SendMailSystem(emailSender, hostEnvironment);
            cvRepo = new CvRepo(_dbcontext);
            user_id = (int)session.GetInt32("user_id");
            role = session.GetString("role");

        }
        public async Task<IActionResult> Candidate(int id)
        {

            if (Checkinfor(id))
            {
                TempData["user_id"] = user_id;
                TempData["name"] = session.GetString("name");
                TempData["avatar"] = session.GetString("avatar");
                User user = await _userRepo.SearchUserById(id);
                return View(user);
            }
            return BadRequest();
        }
        public async Task<IActionResult> Admin(int id)
        {

            if (Checkinfor(id))
            {
                TempData["user_id"] = user_id;
                TempData["avatar"] = session.GetString("avatar");
                TempData["name"] = session.GetString("name");
                User user = await _userRepo.SearchUserById(id);
                return View(user);
            }
            return BadRequest();
        }
        public async Task<IActionResult> Company(int id)
        {
            User user = await _userRepo.SearchUserById(id);
            if (Checkinfor(user.user_id))
            {
                TempData["user_id"] = user_id;
                TempData["name"] = user.Name;
                return View(user);
            }

            return NotFound();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] User user)
        {
            if (Checkinfor(id))
            {
                HandleUpdateProfile(user);
                return RedirectToAction("Index", "Profile", id = id);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRecruiterProfile(int id, [FromForm] RecruiterViewModel Recruiter)
        {
            if (Checkinfor(id))
            {
                User user = await _userRepo.SearchUserById(id);
                HandleUpdateRecruiter(user, Recruiter);
                return RedirectToAction("Index", "Profile", id = id);
            }
            return NotFound();
        }

        private void HandleUpdateRecruiter(User user, RecruiterViewModel recruiter)
        {
            user.Name = recruiter.Full_Name;
            user.Email = recruiter.Email;
            user.Phone = recruiter.Phone;
            user.Gender = recruiter.Gender;
            _dbcontext.Update(user);
            _dbcontext.SaveChanges();
        }

        [HttpPost]
        public async Task<IActionResult> DownloadUserCv(int user_profile_id)
        {
            int user_id = (int)session.GetInt32("user_id");
            string role = session.GetString("role");
            if (user_id != null && role != null)
            {
                CV user_cv = await cvRepo.SearchCvOfUser(user_profile_id);
                MemoryStream memory = _sendMailSystem.DownloadSingleFile(user_cv);
                return File(memory.ToArray(), "application/zip", user_cv.user.Name);
            }
            return BadRequest();
        }
        public async void UploadCV([FromForm] IFormFile Resume)
        {
            User user = await _userRepo.SearchUserById(user_id);
           
            string type = Path.GetFileName(Resume.FileName);
            type = type.Substring(type.LastIndexOf(".")).ToUpper();
            if (type == ".PDF")
            {
                HandleChangesCv(user,Resume);
            }
            else
            {
                
            }
        }

        private async void HandleChangesCv(User user, IFormFile resume)
        {
            CV resume_new = await cvRepo.SearchCvOfUser(user.user_id);
            FileSupport.Instance.DeleteFileAsync(resume_new.cv_file, "Resume");
            string filename = await FileSupport.Instance.SaveFileAsync(resume, "Resume");
            resume_new.cv_file = filename;
            _dbcontext.Update(resume_new);
            _dbcontext.SaveChanges();
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
