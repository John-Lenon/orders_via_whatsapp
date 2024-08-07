using Domain.Enumeradores.Produtos;

namespace Application.Commands.DTO.Produtos
{
    public class CategoriaProdutoCommandDTO
    {
        public string Nome { get; set; }
        public int Prioridade { get; set; }
        public EnumStatusCategoria Status { get; set; }
    }
}
