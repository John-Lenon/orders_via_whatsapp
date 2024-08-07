using Application.Commands.DTO.Produtos;
using Application.Queries.DTO;
using Domain.Entities.Produtos;

namespace Application.Configurations.MappingsApp
{
    public static class ProdutoMap
    {
        public static ProdutoQueryDTO MapToDTO(this Produto produto)
        {
            return new ProdutoQueryDTO
            {
                Descricao = produto.Descricao,
                Prioridade = produto.Prioridade,
                Status = produto.Status,
                CaminhoImagem = produto.CaminhoImagem,
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }

        public static Produto MapToEntity(this ProdutoCommandDTO produto)
        {
            return new Produto
            {
                Descricao = produto.Descricao,
                Prioridade = produto.Prioridade,
                Status = produto.Status.GetValueOrDefault(),
                CaminhoImagem = produto.CaminhoImagem,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }
    }
}
