using Domain.Enumeradores;

namespace Application.Commands.DTO
{
    public class ProdutoCommandDTO
    {
        public int CategoriaId { get; set; }
        public int Prioridade { get; set; }
        public EnumStatusProduto Status { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string CaminhoImagem { get; set; }
    }
}
