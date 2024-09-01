namespace Application.Commands.Interfaces.Base
{
    public interface ICommandServiceBase<TEntityDTO>
    {
        Task DeleteAsync(Guid codigo, bool saveChanges = true);

        Task InsertAsync(TEntityDTO entity, bool saveChanges = true);

        Task UpdateAsync(TEntityDTO entityDto, Guid? codigo = null, bool saveChanges = true);
    }
}
