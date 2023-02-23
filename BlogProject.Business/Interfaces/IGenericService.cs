using BlogProject.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Abstract
{
    public interface IGenericService<TEntity> where TEntity: class, IEntity, new()
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> FindByIdAsync(int id);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
