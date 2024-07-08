using Domain.Entities.Base;
using Domain.Enumeradores.Produtos;

namespace Domain.Entities.Produtos
{
    public class CategoriaAdicionalProduto : EntityBase
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Prioridade { get; set; }
        public EnumStatusCategoria Status { get; set; }

        public ICollection<AdicionalProduto> AdicionaisProdutos { get; set; }
    }
}
