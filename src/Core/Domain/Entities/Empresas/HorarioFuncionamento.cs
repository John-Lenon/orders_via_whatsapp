using Domain.Entities.Base;
using Domain.Enumeradores.Empresas;

namespace Domain.Entities.Empresas
{
    public class HorarioFuncionamento : EntityBase
    {
        public int Hora { get; set; }
        public int Minutos { get; set; }
        public EnumDiaDaSemana DiaDaSemana { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
