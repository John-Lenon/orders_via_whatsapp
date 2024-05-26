
using Application.Queries.DTO.Base;

namespace Application.Queries.DTO.Produto
{
    public class ProdutoFilterDTO : FilterBaseDTO
    {
        public decimal? Preco { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }
        public bool? Ativo { get; set; }
    }
}
