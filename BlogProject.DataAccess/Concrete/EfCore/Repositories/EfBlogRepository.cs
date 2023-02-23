using BlogProject.DataAccess.Abstract;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using BlogProject.DataAccess.Concrete.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.DataAccess.Concrete.EfCore.Repositories
{
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogDal
    {
        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            using (var context = new BlogContext())
            {
                var result = from blog in context.Blogs
                             join categoryblog in context.CategoryBlogs on blog.Id equals categoryblog.BlogId
                             join category in context.Categories on categoryblog.CategoryId equals category.Id
                             where category.Id == categoryId
                             select new Blog()
                             {
                                 AppUser = blog.AppUser,
                                 AppUserId = blog.AppUserId,
                                 CategoryBlogs = blog.CategoryBlogs,
                                 Comments = blog.Comments,
                                 Description = blog.Description,
                                 Id = blog.Id,
                                 ImagePath = blog.ImagePath,
                                 PostedTime = blog.PostedTime,
                                 ShortDescription = blog.ShortDescription
                             };
                return await result.OrderByDescending(x=>x.PostedTime).ToListAsync();
            }
        }

        public async Task<List<Category>> GetCategoriesAsync(int blogId)
        {
            using (var context = new BlogContext())
            {
                var result = from category in context.Categories
                             join categoryBlog in context.CategoryBlogs on category.Id equals categoryBlog.CategoryId
                             join blog in context.Blogs on categoryBlog.BlogId equals blog.Id
                             where blog.Id == blogId
                             select new Category()
                             {
                                 Id = category.Id,
                                 Name = category.Name,
                                 CategoryBlogs = category.CategoryBlogs
                             };
                return await result.OrderByDescending(x=>x.Id).ToListAsync();
            }
        }

        public async Task<List<Blog>> GetLastFiveAsync()
        {
            using (var context = new BlogContext())
            {
                return await context.Blogs.OrderByDescending(x => x.PostedTime).Take(5).ToListAsync();
            }
        }
    }
}
