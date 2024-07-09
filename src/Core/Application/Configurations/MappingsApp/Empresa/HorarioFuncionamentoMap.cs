using Application.Commands.DTO;
using Application.Queries.DTO;
using Domain.Entities.Empresa;

namespace Application.Configurations.MappingsApp
{
    public static class HorarioFuncionamentoMap
    {
        public static HorarioFuncionamentoQueryDTO MapToDTO(this HorarioFuncionamento horarioFuncionamento)
        {
            return new HorarioFuncionamentoQueryDTO
            {
                Codigo = horarioFuncionamento.Codigo,
                Hora = horarioFuncionamento.Hora,
                Minutos = horarioFuncionamento.Minutos,
                DiaDaSemana = horarioFuncionamento.DiaDaSemana
            };
        }


        public static HorarioFuncionamento MapToEntity(this HorarioFuncionamentoCommandDTO horarioFuncionamentoDTO)
        {
            return new HorarioFuncionamento
            {
                Hora = horarioFuncionamentoDTO.Hora,
                Minutos = horarioFuncionamentoDTO.Minutos,
                DiaDaSemana = horarioFuncionamentoDTO.DiaDaSemana,
            };
        }

        public static void MapUpdateEntity(this HorarioFuncionamento horarioFuncionamento,
            HorarioFuncionamentoCommandDTO horarioFuncionamentoDTO)
        {
            horarioFuncionamento.Hora = horarioFuncionamentoDTO.Hora;
            horarioFuncionamento.Minutos = horarioFuncionamentoDTO.Minutos;
            horarioFuncionamento.DiaDaSemana = horarioFuncionamentoDTO.DiaDaSemana;
        }
    }
}
