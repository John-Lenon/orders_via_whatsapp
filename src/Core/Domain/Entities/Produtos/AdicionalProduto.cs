using Domain.Entities.Base;
using Domain.Enumeradores;

namespace Domain.Entities.Produtos
{
    public class AdicionalProduto : EntityBase
    {
        public int CategoriaAdicionalProdutoId { get; set; }
        public EnumStatusProduto Status { get; set; }
        public int Prioridade { get; set; }
        public decimal Preco { get; set; }

        public CategoriaAdicionalProduto CategoriaAdicionalProduto { get; set; }
    }
}
