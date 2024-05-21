﻿using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace finalyearproject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDBcontext _dbcontext;
        private ISession session;
        private PostRepo postRepo;
        private UserRepo userRepo;
        private ReportRepo reportRepo;
        private int user_id;
        private string role;
        public AdminController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor) 
        { 
            _dbcontext = dbContext;
            postRepo = new PostRepo(_dbcontext);
            userRepo = new UserRepo(_dbcontext);
            reportRepo = new ReportRepo(_dbcontext);
            session = httpContextAccessor.HttpContext.Session;
            user_id = (int)session.GetInt32("user_id");
            role = session.GetString("role");
        }
        public async Task<IActionResult> Index()
        {
            List<Post> posts = await postRepo.SearchAllPostForAdmin();
            TempData["user_id"] = user_id;
            TempData["role"] = role;
            TempData["avatar"] = session.GetString("avatar");
            TempData["name"] = session.GetString("name");
            return View(posts);
        }
        public async Task<IActionResult> PostManagement(int user_id)
        {
            List<Post> posts = await postRepo.SearchAllPostForAdmin();
            return View(posts);
        }
        public IActionResult Dashboard()
        {
          
            TempData["user_id"] = user_id;
            TempData["role"] = role;
            TempData["avatar"] = session.GetString("avatar");
            TempData["name"] = session.GetString("name");

            return View();
        }

        private async Task<data_chart> HandleGetDataChart()
        {
            List<Report> reports = await reportRepo.SearchAllReport();
            List<User> users = await userRepo.SearchAllWorker();
            List<Post> posts =await postRepo.SearchAllPostForManagement();
            return null;
        }
        public async Task<IActionResult> ManageAccount()
        {
            List<User> allUser = await userRepo.SearchAllUser();
            return View();
        }
        public async Task<IActionResult> UpdateAccount(int user_id)
        {
            User user= await userRepo.SearchUserById(user_id);
            return View(user);
        }
    }
}
