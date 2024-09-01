using Domain.Enumeradores.Empresas;

namespace Application.Commands.DTO
{
    public class HorarioFuncionamentoCommandDTO
    {
        public int Hora { get; set; }
        public int Minutos { get; set; }
        public EnumDiaDaSemana? DiaDaSemana { get; set; }
    }
}
