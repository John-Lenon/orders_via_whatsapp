namespace Application.Queries.Interfaces.Base
{
    public interface IQueryServiceBase<TFilterDTO, TQueryDTO> where TFilterDTO : class where TQueryDTO : class
    {
        Task<IEnumerable<TQueryDTO>> GetAsync(TFilterDTO filter = null);
        Task<TQueryDTO> GetByCodigoAsync(Guid codigo);
    }
}
