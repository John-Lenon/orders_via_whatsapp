namespace Application.Queries.Interfaces.Base
{
    public interface IQueryServiceBase<TFilterDTO, TQueryDTO>
    {
        Task<IEnumerable<TQueryDTO>> GetAsync(TFilterDTO filter);
        Task<TQueryDTO> GetByCodigoAsync(Guid codigo);
    }
}
