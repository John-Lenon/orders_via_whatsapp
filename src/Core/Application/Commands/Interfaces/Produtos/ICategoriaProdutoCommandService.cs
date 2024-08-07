using Application.Commands.DTO.Produtos;
using Application.Commands.Interfaces.Base;

namespace Application.Commands.Interfaces.Produtos
{
    public interface ICategoriaProdutoCommandService : ICommandServiceBase<CategoriaProdutoCommandDTO>
    {
    }
}
