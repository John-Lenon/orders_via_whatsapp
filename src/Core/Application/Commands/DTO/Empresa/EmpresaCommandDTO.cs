using Application.Commands.DTO.Enderecos;
using Domain.Enumeradores.Empresas;

namespace Application.Commands.DTO
{
    public class EmpresaCommandDTO
    {
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string NumeroDoWhatsapp { get; set; }
        public string Email { get; set; }
        public string Dominio { get; set; }

        public EnumStatusDeFuncionamento StatusDeFuncionamento { get; set; }

        public List<HorarioFuncionamentoCommandDTO> HorariosDeFuncionamento { get; set; } = [];

        public EnderecoCommandDTO Endereco { get; set; }
    }
}
