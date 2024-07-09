using Domain.Entities.Base;
using Domain.Enumeradores.Empresa;

namespace Domain.Entities.Empresa
{
    public class Empresa : EntityBase
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string NumeroDoWhatsapp { get; set; }
        public string Email { get; set; }
        public string Dominio { get; set; }
        public string EnderecoDoLogotipo { get; set; }
        public string EnderecoDaCapaDeFundo { get; set; }
        public EnumStatusDeFuncionamento StatusDeFuncionamento { get; set; }
        public List<HorarioFuncionamento> HorariosDeFuncionamento { get; set; }
    }

}
