﻿using Domain.Enumeradores.Empresa;
using System.Text.Json.Serialization;

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
        public string EnderecoDoLogotipo { get; set; }
        public string EnderecoDaCapaDeFundo { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnumStatusDeFuncionamento StatusDeFuncionamento { get; set; }

        public List<HorarioFuncionamentoCommandDTO> HorariosDeFuncionamento { get; set; } = [];
    }
}
