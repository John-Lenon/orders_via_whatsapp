namespace Application.Commands.DTO
{
    public class ProdutoCommandDTO
    {
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }
        public bool Ativo { get; set; }
    }
}
