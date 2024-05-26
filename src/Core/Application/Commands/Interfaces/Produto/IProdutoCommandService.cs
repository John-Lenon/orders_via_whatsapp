using Application.Commands.DTO;
using Application.Commands.Interfaces.Base;

namespace Application.Commands.Interfaces
{
    public interface IProdutoCommandService : ICommandServiceBase<ProdutoCommandDTO>
    {
    }
}
