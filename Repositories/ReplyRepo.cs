using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class ReplyRepo
    {
        private ApplicationDBcontext _dbcontext;
        public ReplyRepo(ApplicationDBcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<Reply_Comment>> GetAllReply_CommentOfComment(int comment_id)
        {
            return await _dbcontext.Replys.Where(rep => rep.comment_id == comment_id).ToListAsync();
        }
        public async Task<Reply_Comment> GetReply(int reply_id)
        {
            return await _dbcontext.Replys.Where(rep=>rep.reply_id==reply_id).FirstAsync();
        }
    }
}
