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
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryDal
    {
        public async Task<List<Category>> GetAllWithCategoryBlogsAsync()
        {
            using (var context = new BlogContext())
            {
                return await context.Categories.OrderByDescending(x => x.Id).Include(x => x.CategoryBlogs).ToListAsync();
            }
        }
    }
}
