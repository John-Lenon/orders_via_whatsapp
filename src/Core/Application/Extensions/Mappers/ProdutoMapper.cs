
using Application.Commands.DTO;
using Application.Queries.DTO.Produto;
using Domain.Entities;

namespace Application.Extensions.Mappers
{
    public static class ProdutoMapper
    {
        public static ProdutoQueryDTO MapToDTO(this Produto produto)
        {
            return new ProdutoQueryDTO
            {
                Ativo = produto.Ativo,
                CaminhoImagem = produto.CaminhoImagem,
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }

        public static IEnumerable<ProdutoQueryDTO> MapToDTO(this IEnumerable<Produto> produtos)
        {
            return produtos.Select(produto => new ProdutoQueryDTO
            {
                Ativo = produto.Ativo,
                CaminhoImagem = produto.CaminhoImagem,
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco
            });
        }

        public static Produto MapToEntity(this ProdutoCommandDTO produto)
        {
            return new Produto
            {
                Ativo = produto.Ativo,
                CaminhoImagem = produto.CaminhoImagem,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }
    }
}
