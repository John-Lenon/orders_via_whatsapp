using Application.Commands.DTO.Produtos;
using Application.Queries.DTO.Produtos;
using Domain.Entities.Produtos;

namespace Application.Configurations.MappingsApp.Produtos
{
    public static class CategoriaProdutoMap
    {
        public static CategoriaProdutoQueryDTO MapToDTO(this CategoriaProduto categoriaProduto)
        {
            return new CategoriaProdutoQueryDTO
            {
                Codigo = categoriaProduto.Codigo,
                Nome = categoriaProduto.Nome,
                Prioridade = categoriaProduto.Prioridade,
                Status = categoriaProduto.Status,
                Produtos = categoriaProduto.Produtos?.Select(x => x.MapToDTO())
            };
        }

        public static CategoriaProduto MapToEntity(this CategoriaProdutoCommandDTO categoriaProduto)
        {
            return new CategoriaProduto
            {
                Nome = categoriaProduto.Nome,
                Prioridade = categoriaProduto.Prioridade,
                Status = categoriaProduto.Status
            };
        }
    }
}
