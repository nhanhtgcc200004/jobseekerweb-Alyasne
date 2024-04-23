namespace finalyearproject.Models.ViewModel
{
    public class Post_CommentViewModel
    {
        public Post_CommentViewModel(Post post,List<Comment> comments)
        {
            this.Post=post;
            this.Comments=comments;
        }
        public Post Post { get; set;}
        public List<Comment> Comments { get; set;}
    }
}
