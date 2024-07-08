using Domain.Entities.Base;
using Domain.Enumeradores.Produtos;

namespace Domain.Entities.Produtos
{
    public class CategoriaProduto : EntityBase
    {
        public string Nome { get; set; }
        public int Prioridade { get; set; }
        public EnumStatusCategoria Status { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
