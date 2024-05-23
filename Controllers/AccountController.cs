using EnterpriceWeb.Controllers;
using finalyearproject.Models;
using finalyearproject.Repositories;
using finalyearproject.SubSystem.Mailutils;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Drawing;

namespace finalyearproject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBcontext _dbContext;
        private readonly UserRepo userRepo;
        private ISession Session;
        private SendMailSystem mailSystem;
        private int user_id;
        private string role;

        public AccountController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            userRepo = new UserRepo(dbContext);
            Session = httpContextAccessor.HttpContext.Session;
            mailSystem = new SendMailSystem(emailSender, hostEnvironment);
            user_id =(int) Session.GetInt32("user_id");
            role = Session.GetString("role");
        }
        public async Task<IActionResult> Index()
        {
            List<User> users = await userRepo.SearchAllUser();
            TempData["user_id"] = user_id;
            TempData["role"] = role;
            TempData["avatar"] = Session.GetString("avatar");
            TempData["name"] = Session.GetString("name");
            return View(users);
        }
        public async Task<IActionResult> AccountDetail(int user_id)
        {
            User user = await userRepo.SearchUserById(user_id);
            TempData["user_id"] = Session.GetInt32("user_id");
            TempData["role"] = role;
            TempData["avatar"] = Session.GetString("avatar");
            TempData["name"] = Session.GetString("name");
            if (role == "Recruiter")
            {
                TempData["Layout"] = "RecruiterLayout";
            }
            else if (role=="Admin")
            {
                TempData["Layout"] = "AdminLayout";
            }
            else
            {
                TempData["Layout"] = "UserLayout";
            }
            
            
            if(user.role =="Recruiter")
            {
                return View("AccountDetailRecruiter",user);
            }
            else if(user.role=="Admin")
            {
                return View("AccountDetailAdmin", user);
            }
            else
            {
                return View(user);
            }
        }
        [HttpPost]
        public void BanAccount(int user_id,string reason)
        {
            if (checkUser())
            {
                HandleBanUser(user_id,reason);
            }
        }

        private async Task HandleBanUser(int user_id,string reason)
        {
            User user = await userRepo.SearchUserById(user_id);
            user.Status = "Banned";
            await mailSystem.SendMailBanAccount(user.Email,reason);
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }
        [HttpPost]
        public async Task UpdateRole(int user_id, string role)
        {
            if (checkUser())
            {
                HandleUpdateRole(user_id, role);
            }
        }

        private async Task HandleUpdateRole(int user_id, string role)
        {
            User user = await userRepo.SearchUserById(user_id);
            user.role=role;
            await mailSystem.SendMailUpdateRoleAccount(user.Email, role);
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }

        private bool checkUser()
        {
            if(user_id !=null && role == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
