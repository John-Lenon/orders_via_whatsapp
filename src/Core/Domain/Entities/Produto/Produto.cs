namespace Domain.Entities.Produto
{
    public class Produto
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }
        public bool Ativo { get; set; }
    }
}
