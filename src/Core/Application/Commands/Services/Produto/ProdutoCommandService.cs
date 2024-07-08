using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Domain.Entities.Produtos;
using Domain.Interfaces.Repositories;

namespace Application.Commands.Services
{
    public class ProdutoCommandService(IServiceProvider serviceProvider) :
        CommandServiceBase<Produto, ProdutoCommandDTO, IProdutoRepository>(serviceProvider),
        IProdutoCommandService
    {
        protected override Produto MapToEntity(ProdutoCommandDTO entityDTO) => entityDTO.MapToEntity();
    }
}
