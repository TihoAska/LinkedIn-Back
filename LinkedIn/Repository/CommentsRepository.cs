using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Repository.IRepository;

namespace LinkedIn.Repository
{
    public class CommentsRepository : GenericRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }
    }
}
