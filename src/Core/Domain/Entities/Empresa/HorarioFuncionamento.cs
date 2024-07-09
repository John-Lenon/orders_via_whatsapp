using Domain.Entities.Base;
using Domain.Enumeradores.Empresa;

namespace Domain.Entities.Empresa
{
    public class HorarioFuncionamento : EntityBase
    {
        public int Id { get; set; }
        public int Hora { get; set; }
        public int Minutos { get; set; }
        public EnumDiaDaSemana DiaDaSemana { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }

}
