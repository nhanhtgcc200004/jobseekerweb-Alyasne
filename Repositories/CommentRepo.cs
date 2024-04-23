using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class CommentRepo
    {
        private ApplicationDBcontext _dbcontext;
        public CommentRepo(ApplicationDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        }
        public async Task<List<Comment>> GetAllCommentByPostId(int postId)
        {
            return await _dbcontext.Comments.Where(c => c.post_id == postId).ToListAsync();
        }
        public async Task<Reply_Comment> GetReplyCommentByCommentId(int reply_id)
        {
            return await _dbcontext.Replys.Where(rep => rep.reply_id == reply_id).FirstOrDefaultAsync();
        }
        public async Task<Comment> GetCommentById(int comment_id)
        {
            return await _dbcontext.Comments.Where(c => c.comment_id == comment_id).FirstOrDefaultAsync();
        }
    }
}
