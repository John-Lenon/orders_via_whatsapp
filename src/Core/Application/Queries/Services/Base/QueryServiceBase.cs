using Domain.Entities.Base;
using Domain.Interfaces.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.Queries.Services.Base
{

    public abstract class QueryServiceBase<TRepository, TQueryDTO, TEntity>
        where TEntity : EntityBase, new()
        where TRepository : IRepositoryBase<TEntity>
        where TQueryDTO : class, new()
    {
        protected readonly TRepository _repository;

        public QueryServiceBase(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<TRepository>();
        }

        public virtual TEntity GetByCodigo(Guid codigo)
        {
            return _repository.Get(entity => entity.Codigo == codigo).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAsync(TQueryDTO filter)
        {
            return _repository.Get(GetFilterExpression(filter));
        }

        protected abstract Expression<Func<TEntity, bool>> GetFilterExpression(TQueryDTO filter);
    }
}
