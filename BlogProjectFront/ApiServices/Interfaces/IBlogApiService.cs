using System.Collections.Generic;
using System.Threading.Tasks;
using BlogProjectFront.Models;

namespace BlogProjectFront.ApiServices.Interfaces
{
    public interface IBlogApiService{
        Task<List<BlogListModel>> GetAllAsync();
        Task<BlogListModel> GetDetailById(int id);
        Task<List<BlogListModel>> GetAllByCategoryId(int categoryId);
        Task AddAsync(BlogAddModel model);
        Task UpdateAsync(BlogUpdateModel model);
        Task DeleteAsync(int id);
        Task<List<CategoryListModel>> GetCategoriesAsync(int id);
        Task<List<BlogListModel>> GetLastFiveAsync();
        Task AddToCategoryAsync(CategoryBlogModel categoryBlogModel);
        Task DeleteFromCategoryAsync(CategoryBlogModel categoryBlogModel);
        Task<List<BlogListModel>> SearchAsync(string s);
        Task<List<CommentListModel>> GetCommentsAsync(int blogId, int? parentCommentId);
        Task AddCommentAsync(CommentAddModel commentAddModel);
    }
}