namespace finalyearproject.Models.ViewModel
{
    public class data_chart
    {
        public List<Post> posts {  get; }
        public List<User> users {  get; }
        public List<Report> report {  get; }
        public data_chart(List<Post> posts, List<User> users, List<Report> reports) 
        { 
            this.posts = posts;
            this.users = users;
            report = reports;
        }

    }
}
