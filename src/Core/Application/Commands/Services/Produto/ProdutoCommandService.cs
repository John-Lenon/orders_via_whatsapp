using Application.Commands.Services.Base;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Commands.Services
{
    public class ProdutoCommandService(IServiceProvider serviceProvider) :
        CommandServiceBase<Produto, IProdutoRepository>(serviceProvider)
    {

    }
}
