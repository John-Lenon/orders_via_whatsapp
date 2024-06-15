
using Application.Queries.DTO.Base;

namespace Application.Queries.DTO
{
    public class EmpresaFilterDTO : FilterBaseDTO
    {
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
    }
}
