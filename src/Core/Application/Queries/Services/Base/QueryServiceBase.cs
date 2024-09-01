using Application.Queries.DTO.Base;
using Application.Queries.Interfaces.Base;
using Application.Utilities;
using Domain.Entities.Base;
using Domain.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.Queries.Services.Base
{
    public abstract class QueryServiceBase<TRepository, TFilterDTO, TQueryDTO, TEntity>(IServiceProvider serviceProvider) :
        IQueryServiceBase<TFilterDTO, TQueryDTO>
        where TEntity : EntityBase, new()
        where TFilterDTO : FilterBaseDTO
        where TRepository : IRepositoryBase<TEntity>
        where TQueryDTO : QueryBaseDTO, new()
    {
        protected readonly TRepository _repository = serviceProvider.GetRequiredService<TRepository>();

        public virtual async Task<TQueryDTO> GetByCodigoAsync(Guid codigo)
        {
            var result = await _repository.Get(entity => entity.Codigo == codigo).FirstOrDefaultAsync();
            return MapToDTO(result);
        }

        public virtual async Task<IEnumerable<TQueryDTO>> GetAsync(TFilterDTO filter = null)
        {
            var listResult = await _repository.Get(GetFilterExpression(filter)).ToListAsync();
            return listResult.Select(MapToDTO);
        }

        protected abstract Expression<Func<TEntity, bool>> GetFilterExpression(TFilterDTO filter);

        protected virtual TQueryDTO MapToDTO(TEntity entity) => entity.MapToDTO<TEntity, TQueryDTO>();
    }
}
