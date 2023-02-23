using BlogProject.Business.Abstract;
using BlogProject.DataAccess.Abstract;
using BlogProject.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class CategoryManager: GenericManager<Category>, ICategoryService
    {
        private IGenericDal<Category> _genericDal;
        private ICategoryDal _categoryDal;
        public CategoryManager(IGenericDal<Category> genericDal,ICategoryDal categoryDal) : base(genericDal)
        {
            _categoryDal = categoryDal;
            _genericDal = genericDal;
        }

        public async Task<List<Category>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDal.GetAllAsync(x => x.Id);
        }

        public async Task<List<Category>> GetAllWithCategoryBlogsAsync()
        {
            return await _categoryDal.GetAllWithCategoryBlogsAsync();
        }
    }
}
