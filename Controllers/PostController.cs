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
            List<Post> posts = await postRepo.SearchAllPost();
            return View(posts);
        }
        public async Task<IActionResult> Detail(int post_id)
        {
            List<Comment> comments = await commentRepo.GetAllCommentByPostId(post_id);
            if (CheckUserInfo())
            {
                Post post = await postRepo.SearchPostById(post_id);
                Post_CommentViewModel post_comment= new Post_CommentViewModel(post,comments);
                return View(post_comment);
            }
            else return NotFound();
        }
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost([FromForm] Post post)
        {
            if (CheckUserInfo())
            {
                HandleCreatePost(post);
                return View();
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> UpdatePost(int post_id)
        {
            Post post=await postRepo.SearchPostById(post_id);
            if (post != null&& user_id!=null && user_id==post.user_id)
            {
                return View();
            }
            else { return NotFound(); }
        }
        public async Task<IActionResult> UpdatePost([FromForm] Post post)
        {
            HandleUpdatePost(post);
            return RedirectToAction("Detail","Post", new {post_id=post.post_id});
        }
        public async Task<IActionResult> AddComment(int post_id, [FromForm] Comment comment)
        {
            return View();
        }
        public async void UpdateComment(int comment_id,string newcontent,string type_comment)
        {
            if (type_comment=="reply")
            {
                Reply_Comment reply = await commentRepo.GetReplyCommentByCommentId(comment_id);
                HandleUpdateComment(newcontent,reply,"reply");
            }
            else
            {
                Comment comment = await commentRepo.GetCommentById(comment_id);
                HandleUpdateComment(newcontent, comment,"comment");
            }
            
        }

        private void HandleUpdateComment(string newcontent,Object comment,string type)
        {
            if (type == "reply")
            {
                Reply_Comment reply = (Reply_Comment)comment;
                reply.reply_content = newcontent;
                _dbContext.Update(reply);
                _dbContext.SaveChanges();
            }
            else
            {
                Comment newcomment = (Comment)comment;
                newcomment.comment_content = newcontent;
                _dbContext.Update(newcomment);
                _dbContext.SaveChanges();
            }
           
        }

        private void HandleUpdatePost(Post post)
        {
            _dbContext.Update(post);
            _dbContext.SaveChanges();
        }

        private bool CheckUserInfo()
        {
           if(user_id!=null&& role != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void HandleCreatePost(Post post)
        {
            _dbContext.Add(post);
            _dbContext.SaveChanges();
        }
       
        [HttpPost]
        public async void DeletePost(int id)
        {
            User user=await userRepo.SearchUserById(id);
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }
        [HttpPost]
        public async Task<IActionResult> AppliedJob(int post_id)
        {
            HandleAppliedJob(post_id);
            return Ok();
        }

        private void HandleAppliedJob(int post_id)
        {
            Appliedjob appliedjob = new Appliedjob();
            appliedjob.post_id = post_id;
            appliedjob.user_id= user_id;
            appliedjob.status = "in Processing...";
            _dbContext.Add(appliedjob);
            _dbContext.SaveChanges();
        }
    }
}
