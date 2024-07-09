using Application.Queries.DTO.Base;
using Domain.Enumeradores.Empresa;

namespace Application.Queries.DTO
{
    public class EmpresaQueryDTO : QueryBaseDTO
    {
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string NumeroDoWhatsapp { get; set; }
        public string Email { get; set; }
        public string Dominio { get; set; }
        public string EnderecoDoLogotipo { get; set; }
        public string EnderecoDaCapaDeFundo { get; set; }
        public EnumStatusDeFuncionamento StatusDeFuncionamento { get; set; }
        public List<HorarioFuncionamentoQueryDTO> HorariosDeFuncionamento { get; set; }
    }
}
