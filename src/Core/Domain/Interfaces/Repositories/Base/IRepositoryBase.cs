using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByCodigoAsync(Guid? codigo);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(TEntity[] entity);
        Task<bool> SaveChangesAsync();
    }
}
