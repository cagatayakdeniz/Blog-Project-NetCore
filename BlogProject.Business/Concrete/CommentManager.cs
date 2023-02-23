using BlogProject.Business.Abstract;
using BlogProject.DataAccess.Abstract;
using BlogProject.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class CommentManager: GenericManager<Comment>, ICommentService
    {
        private IGenericDal<Comment> _genericDal;
        private ICommentDal _commentDal;
        public CommentManager(IGenericDal<Comment> genericDal, ICommentDal commentDal) : base(genericDal)
        {
            _commentDal = commentDal;
            _genericDal = genericDal;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            return await _commentDal.GetAllWithSubCommentsAsync(blogId, parentId);
        }
    }
}
