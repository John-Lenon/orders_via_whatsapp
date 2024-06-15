using Domain.Enumeradores.Empresa;
using System.Text.Json.Serialization;

namespace Application.Commands.DTO
{
    public class HorarioFuncionamentoCommandDTO
    {
        public int Hora { get; set; }
        public int Minutos { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnumDiaDaSemana DiaDaSemana { get; set; }
    }
}
