﻿using EnterpriceWeb.Controllers;
using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
using finalyearproject.Repositories;
using finalyearproject.SubSystem.Mailutils;
using finalyearproject.SubSystem.Support;
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
        public IActionResult Register()
        {
            if (Session.GetString("role") != null && Session.GetInt32("user_id") != null)
            {
                if (Session.GetString("role") == "Recruiter")
                {
                    return RedirectToAction("Recruiter", "Home");
                }
                else if (Session.GetString("role") == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }
        public IActionResult Login() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await userRepo.Login(email, password);
            var login = HandleLogin(user);
            if (login == "success")
            {
                TempData["user_id"] = user.user_id;
                if (user.role == "Recruiter")
                {
                    TempData["avatar"] = user.avatar;
                    TempData["name"] = user.Name;
                    return RedirectToAction("Recruiter", "Home");
                }
                else if (user.role == "Admin")
                {
                    TempData["avatar"] = user.avatar;
                    TempData["name"] = user.Name;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["avatar"] = user.avatar;
                    TempData["name"] = user.Name;
                    return RedirectToAction("Candidate", "Home");
                }
            }
            else
            {
                ViewBag.erorr = "Email or password is incorrect";
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
                    Session.SetString("avatar", user.avatar);
                    Session.SetString("name", user.Name);
                    TempData["role"] = Session.GetString("role");
                    TempData["name"] = user.Name;
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
        [HttpPost]
        public async Task<IActionResult> CandidateRegister([FromForm] User user,[FromForm] IFormFile Resume)
        {
            string type = Path.GetFileName(Resume.FileName);
            type = type.Substring(type.LastIndexOf("."));
            if (type==".PDF")
            {
                var _user = await userRepo.Register(user.Email);
                if (user == null)
                {
                    //_user.us_password = MD5(_user.us_password);
                    
                   
                    HandleRegister(user, Resume);
                    //User new_user = await userRepo.SearchUserJustInsert();
                    //String verify_code = HandleAddVerifyCode(new_user);
                    //mailSystem.SendVerifyCode(verify_code, new_user.Email);
                    //return RedirectToAction("VerifyAccount", "Authentication", new {id=new_user.user_id });
                }
                else
                {

                }
            }
            else
            {

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

        

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (email != null)
            {
             User user1 = await userRepo.SearchUserByMail(email);
                if (user1 != null)
                {
                    string newpassword = await mailSystem.SendgmailForgetPassword(email);
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
        public async Task<IActionResult> ChangePassword(string current_password, string new_passsword, string confirm_password)
        {
            int id = 1;
            if (checkUser(id))
            {
                User user=await userRepo.SearchUserById(id);
                if (checkPassword(user,current_password,new_passsword,confirm_password))
                {
                    changesPassword(new_passsword, user);
                    
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

        private async void HandleRegister(User user,IFormFile avatar)
        {
            try
            {
                if (avatar != null)
                {
                    string filename = await FileSupport.Instance.SaveFileAsync(avatar, "img/Avatar");
                    user.avatar = "img_avatar" ;
                    user.role = "user";
                    user.Status = "1";
                    user.company_id = 999;
                    _dbContext.Add(user);
                    _dbContext.SaveChanges();
                    CV Resume = new CV();
                    Resume.date_upload = DateTime.Now;
                    Resume.cv_file= filename;
                    Resume.user_id = user.user_id;
                    _dbContext.Add(Resume);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
       
        }
        [HttpPost]
        public IActionResult RegisterCompany([FromForm] RecruiterViewModel recruiter, IFormFile Logo)
        {
            User user = new User();
            Company company = new Company();
            HandleRegisterCompany(user,company,recruiter,Logo);
            return View();
        }
       
        private async void HandleRegisterCompany(User user,Company company,RecruiterViewModel recruiter, IFormFile Logo)
        {
            try
            {
                if (Logo != null)
                {
                    string filename = await FileSupport.Instance.SaveFileAsync(Logo, "img/Logo");
                    company.status = "OK";
                    company.Address = recruiter.Address;
                    company.company_name = recruiter.Company_name;
                    company.Email_conpany = recruiter.Email;
                    _dbContext.Add(company);
                    _dbContext.SaveChanges();
                    user.avatar = filename;
                    user.Email = recruiter.Email;
                    user.role = "User";
                    user.Phone = recruiter.Phone;
                    user.Gender = recruiter.Gender;
                    user.Password = recruiter.Password;
                    user.company_id = company.conpany_id;
                    _dbContext.Add(user);
                    _dbContext.SaveChanges();
                    _dbContext.Add(user);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }

       
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
            return RedirectToAction("Authentication");
        }
    }
}
