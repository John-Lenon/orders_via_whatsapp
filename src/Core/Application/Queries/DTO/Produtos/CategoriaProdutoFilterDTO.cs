using Application.Queries.DTO.Base;
using Domain.Enumeradores.Produtos;

namespace Application.Queries.DTO.Produtos
{
    public class CategoriaProdutoFilterDTO : FilterBaseDTO
    {
        public string Nome { get; set; }
        public int? Prioridade { get; set; }
        public EnumStatusCategoria? Status { get; set; }
    }
}
