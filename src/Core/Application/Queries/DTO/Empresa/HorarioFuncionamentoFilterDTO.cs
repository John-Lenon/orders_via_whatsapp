﻿
using Application.Queries.DTO.Base;
using Domain.Enumeradores.Empresa;

namespace Application.Queries.DTO
{
    public class HorarioFuncionamentoFilterDTO : FilterBaseDTO
    {
        public int Hora { get; set; }
        public int Minutos { get; set; }
        public EnumDiaDaSemana DiaDaSemana { get; set; }
    }
}
