namespace Application.Queries.DTO.Produto
{
    public class ProdutoQueryDTO
    {
        public Guid? Codigo { get; set; }
        public decimal? Preco { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }
        public bool? Ativo { get; set; }
    }
}
