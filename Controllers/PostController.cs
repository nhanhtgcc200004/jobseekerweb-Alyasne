using finalyearproject.Models;
using finalyearproject.Models.ViewModel;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDBcontext _dbContext;
        private readonly UserRepo userRepo;
        private PostRepo postRepo;
        private CommentRepo commentRepo;
        private ISession Session;
        private int user_id;
        private string role;
        public PostController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            userRepo = new UserRepo(dbContext);
            postRepo = new PostRepo(dbContext);
            commentRepo = new CommentRepo(dbContext);
            Session = httpContextAccessor.HttpContext.Session;
            user_id = (int) Session.GetInt32("user_id");
            role = Session.GetString("role");
            
        }
        public async Task<IActionResult> Index()
        {
            List<Post> posts = await postRepo.SearchAllPostForHome();
            return View(posts);
        }
        public async Task<IActionResult> PostManagement(int user_id)
        {
            List<Post> posts= await postRepo.SearchAllPostByUserId(user_id);
            return View(posts);
        }
        
        public async Task<IActionResult> Detail(int post_id)
        {
            User user = await userRepo.SearchUserById(user_id);
            List<Comment> comments = await commentRepo.GetAllCommentByPostId(post_id);
            if (CheckUserInfo())
            {
                Post post = await postRepo.SearchPostById(post_id);
                Post_CommentViewModel post_comment= new Post_CommentViewModel(post,comments);
                TempData["role"] = role;
                TempData["avatar"] = user.avatar;
                return View(post_comment);
            }
            else return NotFound();
        }
        public async Task<IActionResult>DetailAdmin(int post_id)
        {
            List<Comment> comments = await commentRepo.GetAllCommentByPostId(post_id);
            if (CheckUserInfo())
            {
                Post post = await postRepo.SearchPostById(post_id);
                Post_CommentViewModel post_comment = new Post_CommentViewModel(post, comments);
                TempData["role"] = role;
                return View(post_comment);
            }
            else return NotFound();
        }

        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] Post post)
        {
            if (CheckUserInfo())
            {
              
                User user = await userRepo.SearchUserById(user_id);
                HandleCreatePost(post,user);
                return View();
            }
            else
            {
                TempData.Clear();
                TempData["new-post"] = post;
                return View();
            }
        }
        public async Task<IActionResult> UpdatePost(int post_id)
        {
            Post post=await postRepo.SearchPostById(post_id);
           
            if (post != null && user_id != null)
            {
                return View(post);
            }
            else { return NotFound(); }
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost([FromForm] Post form_post, int post_id)
        {
            Post post = await postRepo.SearchPostById(post_id);
            HandleUpdatePost(HandlePost(post,form_post));
            return RedirectToAction("PostManagement", "Post");
        }

        private Post HandlePost(Post post, Post form_post)
        {
            post.Position = form_post.Position;
            post.salary = form_post.salary;
            post.experience = form_post.experience;
            post.limit_candidates = form_post.limit_candidates;
            post.expired_date = form_post.expired_date;
            post.job_description = form_post.job_description;
            post.skill_required= form_post.skill_required;
            post.other_condition = form_post.other_condition;
            return post;
        }

        public async Task<IActionResult> AddComment(int post_id, [FromForm] Comment comment)
        {
            return View();
        }
        //public async void UpdateComment(int comment_id,string newcontent,string type_comment)
        //{
        //    if (type_comment=="reply")
        //    {
        //        //Reply_Comment reply = await commentRepo.GetReplyCommentByCommentId(comment_id);
        //        HandleUpdateComment(newcontent,reply,"reply");
        //    }
        //    else
        //    {
        //        Comment comment = await commentRepo.GetCommentById(comment_id);
        //        HandleUpdateComment(newcontent, comment,"comment");
        //    }
            
        //}

        //private void HandleUpdateComment(string newcontent,Object comment,string type)
        //{
        //    if (type == "reply")
        //    {
        //        Reply_Comment reply = (Reply_Comment)comment;
        //        reply.reply_content = newcontent;
        //        _dbContext.Update(reply);
        //        _dbContext.SaveChanges();
        //    }
        //    else
        //    {
        //        Comment newcomment = (Comment)comment;
        //        newcomment.comment_content = newcontent;
        //        _dbContext.Update(newcomment);
        //        _dbContext.SaveChanges();
        //    }
           
        //}

        private void HandleUpdatePost(Post post)
        {
            _dbContext.Update(post);
            _dbContext.SaveChanges();
        }

        private bool CheckUserInfo()
        {
            //if(user_id!=null&& role != null)
            // {
            //     return true;
            // }
            // else
            // {
            //     return false;
            // }
            return true;
        }

        private void HandleCreatePost(Post post,User user)
        {
            post.date_post = DateTime.Now;
            post.user_id = user_id;
            post.address = user.conpany.Address;
            post.status = "1";
            post.post_title = "a";
            _dbContext.Add(post);
            _dbContext.SaveChanges();
        }
       
        [HttpPost]
        public async void DeletePost(int post_id)
        {
            Post post= await postRepo.SearchPostById(post_id);
            post.status = "deleted";
            _dbContext.Update(post);
            _dbContext.SaveChanges();
        }
        [HttpPost]
        public async Task<IActionResult> AppliedJob(int post_id)
        {
            Post post_targeted = await postRepo.SearchPostById(post_id);
            if (post_targeted.limit_candidates >= post_targeted.total_of_candidates)
            {
                return Ok("out of candidates");
            }
            else
            {
                HandleAppliedJob(post_targeted);
                return Ok();
            }
        }
        private async void HandleAppliedJob(Post post_targeted)
        {
            Appliedjob appliedjob = new Appliedjob();
            appliedjob.post_id = post_targeted.post_id;
            post_targeted.total_of_candidates++;
            appliedjob.user_id= user_id;
            appliedjob.status = "in Processing...";
            _dbContext.Add(appliedjob);
            _dbContext.Update(post_targeted);
            _dbContext.SaveChanges();
        }
    }
}
