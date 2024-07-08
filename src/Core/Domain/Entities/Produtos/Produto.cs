using Domain.Entities.Base;
using Domain.Enumeradores;

namespace Domain.Entities.Produtos
{
    public class Produto : EntityBase
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public int Prioridade { get; set; }
        public EnumStatusProduto Status { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }

        public CategoriaProduto CategoriaProduto { get; set; }
    }
}
