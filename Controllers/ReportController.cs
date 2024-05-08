using EnterpriceWeb.Controllers;
using finalyearproject.Models;
using finalyearproject.Repositories;
using finalyearproject.SubSystem.Mailutils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace finalyearproject.Controllers
{
    public class ReportController : Controller
    {
        private ISession session;
        private ReportRepo _reportRepo;
        private ApplicationDBcontext _dbcontext;
        private PostRepo _postRepo;
        private UserRepo _userRepo;
        private SendMailSystem mailSystem;
        private int user_id;
        private string role;
        public ReportController(ApplicationDBcontext dBcontext, HttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = dBcontext;
            _reportRepo = new ReportRepo(_dbcontext);
            _postRepo = new PostRepo(_dbcontext);
            _userRepo=new UserRepo(_dbcontext);
            session = httpContextAccessor.HttpContext.Session;
            mailSystem=new SendMailSystem(emailSender, hostEnvironment);
            user_id = (int)session.GetInt32("user_id");
            role = session.GetString("role");
        }
        public async Task<IActionResult> index()
        {
            List<Report> reports=await _reportRepo.SearchAllReport();
            return View(reports);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReport(int reporter_id,int post_id, [FromForm] Report report)
        {
            
            if (CheckInfor())
            {
                report.post_id = post_id;
                report.reporter_id = user_id;
                report.reporter_id = reporter_id;
                HandleCreateReport(report);
                return View("index");
            }
            return BadRequest();
        }
        [HttpDelete]
        public async void AcceptReport(int report_id)
        {
            if (CheckInfor())
            {
                Report report = await _reportRepo.SearchReportById(report_id);
                UpdateReport(report,"Accepted");
                Post post = await _postRepo.SearchPostById(report.report_id);
                User reporter = await _userRepo.SearchUserById(report.reporter_id);
                User reciver = await _userRepo.SearchUserById(report.reciver_id);
                HandleAcceptReport(post,reporter,reciver,report);
            }
        }
        [HttpDelete]
        public async void RefuseReport(int report_id)
        {
            if (CheckInfor())
            {
                Report report = await _reportRepo.SearchReportById(report_id);
                UpdateReport(report, "Refuse");
                User reporter = await _userRepo.SearchUserById(report.reporter_id);
                HandleRefuseReport(reporter);
            }
        }
        private void HandleAcceptReport(Post post,User reporter,User reciver,Report report)
        {
            updatePost(post);
            mailSystem.SendMailAcceptReport(reporter,reciver,report);
        }

        private void updatePost(Post post)
        {
            post.status = "reported";
            _dbcontext.Update(post);
            _dbcontext.SaveChanges();
        }
        private void UpdateReport(Report report,string status)
        {
            report.status = status;
            _dbcontext.Update(report);
            _dbcontext.SaveChanges();
        }
        private void HandleRefuseReport(User repoter)
        {
            mailSystem.SendgmailRefuseReport(repoter.Email);
        }
        private bool CheckInfor()
        {
            if (user_id != null)
            {
                return true;
            }
            return false;
        }
        private void HandleCreateReport(Report report)
        {
            _dbcontext.Add(report);
            _dbcontext.SaveChanges();
        }
    }
}
