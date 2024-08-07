using Application.Commands.DTO.Produtos;
using Application.Commands.Interfaces.Produtos;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp.Produtos;
using Domain.Entities.Produtos;
using Domain.Interfaces.Repositories.Produtos;

namespace Application.Commands.Services.Produtos
{
    public class CategoriaProdutoCommandService(IServiceProvider serviceProvider) :
        CommandServiceBase<CategoriaProduto, CategoriaProdutoCommandDTO, ICategoriaProdutoRepository>(serviceProvider), ICategoriaProdutoCommandService
    {
        protected override CategoriaProduto MapToEntity(CategoriaProdutoCommandDTO entityDTO) =>
            entityDTO.MapToEntity();
    }
}
