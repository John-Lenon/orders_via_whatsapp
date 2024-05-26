using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Produto : EntityBase
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }
        public bool Ativo { get; set; }
    }
}
