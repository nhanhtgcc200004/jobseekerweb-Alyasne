using finalyearproject.Models;

namespace finalyearproject.Repositories
{
    public class TagRepo
    {
        private ApplicationDBcontext dbContext;

        public TagRepo(ApplicationDBcontext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
