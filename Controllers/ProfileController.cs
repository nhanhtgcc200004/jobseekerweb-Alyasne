
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
        private CompanyRepo companyRepo;
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
            companyRepo = new CompanyRepo(_dbcontext);
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
                TempData["avatar"] = session.GetString("avatar");
                TempData["name"] = session.GetString("name");
                return View(user);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromForm] User user, IFormFile avatar)
        {
            if (Checkinfor(user_id))
            {
                User old_user = await _userRepo.SearchUserById(user_id);
                HandleUpdateProfile(user,old_user,avatar);
                return RedirectToAction("Candidate", "Profile",new {id=user_id});
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRecruiterProfile([FromForm] RecruiterViewModel Recruiter, IFormFile Logo)
        {
            if (Checkinfor(user_id))
            {
                User user = await _userRepo.SearchUserById(user_id);
                Company company = await companyRepo.SearchConpanyById(user.company_id);
                HandleUpdateRecruiter(user,company,Recruiter ,Logo);
                return RedirectToAction("Company", "Profile", new {id=user_id});
            }
            return NotFound();
        }

        private async Task HandleUpdateRecruiter(User user,Company company, RecruiterViewModel recruiter,IFormFile Logo)
        {
            try
            {

                if (Logo != null)
                {
                    FileSupport.Instance.DeleteFileAsync(user.avatar, "img/avatar");
                    string filename = await FileSupport.Instance.SaveFileAsync(Logo, "img/Avatar");
                    user.avatar = filename;
                }
                user.Name = recruiter.Full_Name;
                user.Email = recruiter.Email;
                user.Phone = recruiter.Phone;
                user.Gender = recruiter.Gender;
                company.Address= recruiter.Address;
                company.company_name = recruiter.Company_name;
                company.Email_conpany = recruiter.Email;
                _dbcontext.Update(user);
                _dbcontext.Update(company);
                _dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        [HttpPost]
        public int SomethingDummy(int user_profile_id)
        {
            return user_profile_id;
        }
        public async Task<IActionResult> DownloadUserCv(int user_profile_id)
        {
            int user_id = (int)session.GetInt32("user_id");
            string role = session.GetString("role");
            if (user_id != null && role != null)
            {
                CV user_cv = await cvRepo.SearchCvOfUser(user_profile_id);
                MemoryStream memory = _sendMailSystem.DownloadSingleFile(user_cv);
                return File(memory.ToArray(), "application/zip", user_cv.user.Name+".zip");
            }
            return BadRequest();
        }
        public async Task UploadCV([FromForm] IFormFile Resume)
        {
            User user = await _userRepo.SearchUserById(user_id);
           
            string type = Path.GetFileName(Resume.FileName);
            type = type.Substring(type.LastIndexOf(".")).ToUpper();
            if (type == ".PDF")
            {
               await HandleChangesCv(user,Resume);
            }
            else
            {
                
            }
        }

        private async Task HandleChangesCv(User user, IFormFile resume)
        {
            CV resume_new = await cvRepo.SearchCvOfUser(user.user_id);
            if (resume_new!=null)
            {
                FileSupport.Instance.DeleteFileAsync(resume_new.cv_file, "Resume");
                string filename = await FileSupport.Instance.SaveFileAsync(resume, "Resume");
                resume_new.cv_file = filename;
                _dbcontext.Update(resume_new);
                await _dbcontext.SaveChangesAsync();

            }
            else
            {
                resume_new = new CV();
                string filename = await FileSupport.Instance.SaveFileAsync(resume, "Resume");
                resume_new.date_upload = DateTime.Now;
                resume_new.user_id=user.user_id;
                resume_new.cv_file = filename;
               await _dbcontext.AddAsync(resume_new);
               await _dbcontext.SaveChangesAsync();

            }
           
        }

        private async Task HandleUpdateProfile(User user,User old_user, IFormFile avatar)
        {
            try
            {
                
                if (avatar != null)
                {
                    FileSupport.Instance.DeleteFileAsync(old_user.avatar, "img/avatar");
                    string filename = await FileSupport.Instance.SaveFileAsync(avatar, "img/Avatar");
                    old_user.avatar = filename;
                }
                old_user.Email = user.Email;
                old_user.Name = user.Name;
                old_user.Birthday = user.Birthday;
                old_user.Phone = user.Phone;
                old_user.Gender = user.Gender;

                _dbcontext.Update(old_user);
                _dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private bool Checkinfor(int id)
        {
            if (user_id != null && user_id == id)
            {
                return true;
            }
            return false;
           ;
        }
    }
}
