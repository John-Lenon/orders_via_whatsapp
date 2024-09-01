using Application.Queries.DTO.Base;
using Domain.Enumeradores.Empresas;
using System.Text.Json.Serialization;

namespace Application.Queries.DTO
{
    public class HorarioFuncionamentoQueryDTO : QueryBaseDTO
    {
        public int Hora { get; set; }
        public int Minutos { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnumDiaDaSemana DiaDaSemana { get; set; }
    }
}
