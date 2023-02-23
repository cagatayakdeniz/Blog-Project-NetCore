using BlogProject.DataAccess.Abstract;
using BlogProject.DataAccess.Concrete.EfCore.Contexts;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Concrete.EfCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(blogId, parentId, result);
            return result;
        }

        private async Task GetComments(int blogId, int? parentId, List<Comment> result)
        {
            using (var context = new BlogContext())
            {
                var comments = await context.Comments.Where(x => x.BlogId == blogId && x.ParentCommentId == parentId)
                    .OrderByDescending(x => x.PostedTime).ToListAsync();

                foreach (var comment in comments)
                {
                    if (comment.SubComments == null)
                    {
                        comment.SubComments = new List<Comment>();
                    }

                    await GetComments(comment.BlogId, comment.Id, comment.SubComments);

                    if(!result.Contains(comment))
                    {
                        result.Add(comment);
                    }
                }
            }
        }
    }
}
