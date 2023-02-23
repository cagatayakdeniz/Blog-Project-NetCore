using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Abstract
{
    public interface ICategoryService: IGenericService<Category>
    {
        Task<List<Category>> GetAllSortedByPostedTimeAsync();
        Task<List<Category>> GetAllWithCategoryBlogsAsync();
    }
}
