using Application.Queries.DTO.Enderecos;
using Application.Queries.Interfaces.Base;

namespace Application.Queries.Interfaces.Enderecos
{
    public interface IEnderecoQueryService : IQueryServiceBase<EnderecoFilterDTO, EnderecoQueryDTO>
    {
    }
}
