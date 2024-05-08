using EnterpriceWeb.Controllers;
using finalyearproject.Models;
using finalyearproject.Repositories;
using finalyearproject.SubSystem.Mailutils;
using Microsoft.AspNetCore.Mvc;
using System;


namespace finalyearproject.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDBcontext _dbContext;
        private readonly UserRepo userRepo;
        private ISession Session;
        private SendMailSystem mailSystem;
        private VerifyRepo verifyRepo;
        public AuthenticationController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            userRepo = new UserRepo(dbContext);
            Session = httpContextAccessor.HttpContext.Session;
            mailSystem = new SendMailSystem(emailSender,hostEnvironment);
            verifyRepo = new VerifyRepo(_dbContext);
        }
        
        public IActionResult Login()
        {
            if (Session.GetString("role") != null&& Session.GetInt32("user_id")!=null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await userRepo.Login(email, password);
            var login = HandleLogin(user);
            if (login =="success")
            {
                return RedirectToAction("Index","Post");
            }
            else if (login == "this account still doesn't verify")
            {
                return RedirectToAction("VerifyAccount", "Account");
            }
            else
            {
                ViewBag.erorr = "Gmail or password is incorrect";
                return View();
            }
        }

        private string HandleLogin(User user)
        {
            if (user != null)
            {
                if (user.Status != "waiting for confirmation" && user.Status!="deleted")
                {
                    Session.SetString("role", user.role);
                    Session.SetInt32("user_id", user.user_id);
                    return "success";
                }
                else
                {
                    return "this account still doesn't verify";
                }
            }
            else
            {
                return "fail";
            }

        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] User user)//
        {
            User s_user = await userRepo.SearchUserByMail(user.Email);
            if (CheckValue(s_user))
            {
                user.role = "user";
                user.Status = "waiting for confirmation";
                HandleRegister(user);
                User new_user = await userRepo.SearchUserJustInsert();
                String verify_code=HandleAddVerifyCode(new_user);
                mailSystem.SendVerifyCode(verify_code, new_user.Email);
                return RedirectToAction("VerifyAccount", "Authentication", new {id=new_user.user_id });
            }
            return View();
        }

        public async Task<IActionResult> VerifyAccount(int user_id)
        {
            return View(user_id);
        }
        [HttpPost]
        public async Task<IActionResult> VerifyAccount(int user_id,string verify_code)
        {
            Verification verification= await verifyRepo.SearchVerifyCodeOfUser(user_id);
            if (HandleVerify(verification, verify_code))
            {
                User user= await userRepo.SearchUserById(user_id);
                CompleteVefify(user, verification);
                Session.SetString("role", user.role);
                Session.SetInt32("Id", user.user_id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "your verify code is incorrect";
                return View();
            }
        }

        

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string gmail)
        {
            if (gmail != null)
            {
             User user1 = await userRepo.SearchUserByMail(gmail);
                if (user1 != null)
                {
                    string newpassword = await mailSystem.SendgmailForgetPassword(gmail);
                    if (newpassword != "Something wrong with your account")
                    {
                        changesPassword(newpassword, user1);
                        ViewBag.forgot = "The new password has been sent to the gmail you registered with";
                    }
                    else
                    {
                        ViewBag.forgot = "Something wronG!!!";
                    }
                    return View();
                }
                else
                {
                    ViewBag.forgot = "Gmail not found";
                    return View();
                }
            }
            return View();
        }
       public IActionResult UpdatePassword(int id)
        {
            if (checkUser(id))
            {
                return View();
            }
            return NotFound();
        }

        public async Task<IActionResult> UpdatePassword(int id , string current_password, string new_passsword, string confirm_password)
        {
            if (checkUser(id))
            {
                User user=await userRepo.SearchUserById(id);
                if (checkPassword(user,current_password,new_passsword,confirm_password))
                {
                    changesPassword(new_passsword, user);
                    return RedirectToAction("Index", "Profile", id = id);
                }
                else
                {
                    return View();
                }
            }
            return NotFound();
        }

        private bool checkPassword(User user, string current_password, string new_password, string confirm_password)
        {
            if (current_password.Equals(user.Password))
            {
                if (new_password.Equals(confirm_password))
                {
                    return true;
                }
                ViewBag.Clear();
                ViewBag.erorr = "The new password and confirm_passowrd are not match";
                return false;
            }
            ViewBag.Clear();
            ViewBag.erorr = "The current password is incorrect";
            return false;
        }

        private bool checkUser(int id)
        {
            if (Session.GetString("role") != null && Session.GetInt32("Id")==id)
            {
                return true;
            }
            return false;
        }
        private async void SendVerifyAgain(int user_id)
        {
            Verification verification= await verifyRepo.SearchVerifyCodeOfUser(user_id);
            User user = await userRepo.SearchUserById(user_id);
            verification.verify_code = RandomVerifyCode();
            _dbContext.Update(verification);
            _dbContext.SaveChanges();
            mailSystem.SendVerifyCode(verification.verify_code,user.Email);
        }
        private void changesPassword(string newpassword, User user)
        {
            user.Password = newpassword;
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }

        private bool CheckValue(User user)
        {
            if (user == null)
            {
                return true;
            }
            return false;
        }

        private void HandleRegister(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public IActionResult RegisterCompany()
        {
            return View();
        }
        public IActionResult RegisterCompany([FromForm] Conpany company)
        {
            HandleRegisterCompany(company);
            return View();
        }
       
        private void HandleRegisterCompany(Conpany company)
        {
            company.status = "waiting for confirmation";
            _dbContext.Add(company);
            _dbContext.SaveChanges();
        }
        private string HandleAddVerifyCode(User user)
        {
            Verification verification = new Verification();
            verification.user_id = user.user_id;
            verification.verify_code = RandomVerifyCode();
            _dbContext.Add(verification);
            _dbContext.SaveChanges();
            return verification.verify_code;
        }

        private string RandomVerifyCode()
        {
            Random random = new Random();
            string randomNumber = "";
            for (int i = 0; i < 6; i++)
            {
                randomNumber += random.Next(0, 10);
            }
            return randomNumber;
        }

        private bool HandleVerify(Verification verification,string verify_code)
        {
            if (verification.verify_code == verify_code)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CompleteVefify(User user, Verification verification)
        {
            user.Status = "verified";
            user.Viewable = "Private";
            _dbContext.Update(user);
            _dbContext.Remove(verification);
            _dbContext.SaveChanges();
        }

        public IActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
