using Application.Queries.DTO.Base;
using Domain.Enumeradores;

namespace Application.Queries.DTO
{
    public class ProdutoQueryDTO : QueryBaseDTO
    {
        public int Prioridade { get; set; }
        public EnumStatusProduto Status { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }
    }
}
