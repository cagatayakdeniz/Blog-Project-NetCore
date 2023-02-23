using BlogProject.Business.Abstract;
using BlogProject.DataAccess.Abstract;
using BlogProject.DTO.DTOs;
using BlogProject.Entities.Concrete;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class BlogManager: GenericManager<Blog>, IBlogService
    {
        private IGenericDal<Blog> _genericDal;
        private IGenericDal<CategoryBlog> _categoryBlog;
        private IBlogDal _blogDal;
        public BlogManager(IGenericDal<Blog> genericDal, IGenericDal<CategoryBlog> categoryBlog, IBlogDal blogDal)
            : base(genericDal)
        {
            _blogDal = blogDal;
            _categoryBlog = categoryBlog;
            _genericDal = genericDal;
        }

        public async Task AddToCategory(CategoryBlogDto categoryBlogDto)
        {
            var control = await _categoryBlog.GetAsync(x => x.CategoryId == categoryBlogDto.CategoryId
                                                        && x.BlogId == categoryBlogDto.BlogId);
            if(control==null)
            {
                await _categoryBlog.AddAsync(new CategoryBlog
                {
                    CategoryId = categoryBlogDto.CategoryId,
                    BlogId = categoryBlogDto.BlogId
                });
            }
        }

        public async Task DeleteFromCategory(CategoryBlogDto categoryBlogDto)
        {
            var deletedCategory = await _categoryBlog.GetAsync(x => x.CategoryId == categoryBlogDto.CategoryId
                                                        && x.BlogId == categoryBlogDto.BlogId);
            if(deletedCategory!=null)
            {
                await _categoryBlog.DeleteAsync(deletedCategory);
            }
        }

        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await _blogDal.GetAllByCategoryIdAsync(categoryId);
        }

        public async Task<List<Blog>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDal.GetAllAsync(x => x.PostedTime);
        }

        public async Task<List<Category>> GetCategoriesAsync(int blogId)
        {
            return await _blogDal.GetCategoriesAsync(blogId);
        }

        public async Task<List<Blog>> GetLastFiveAsync()
        {
            return await _blogDal.GetLastFiveAsync();
        }

        public async Task<List<Blog>> SearchAsync(string searchAsync)
        {
            return await _blogDal.GetAllAsync(x => x.Title.Contains(searchAsync) || x.ShortDescription.Contains(searchAsync)
                                                , x => x.PostedTime);
        }
    }
}
