using Application.Commands.DTO;
using Application.Queries.DTO;
using Domain.Entities.Empresa;
using System.Text.RegularExpressions;

namespace Application.Configurations.MappingsApp
{
    public static class EmpresaMap
    {
        public static EmpresaQueryDTO MapToDTO(this Empresa empresa)
        {
            return new EmpresaQueryDTO
            {
                Codigo = empresa.Codigo,
                NomeFantasia = empresa.NomeFantasia,
                RazaoSocial = empresa.RazaoSocial,
                Cnpj = empresa.Cnpj,
                NumeroDoWhatsapp = empresa.NumeroDoWhatsapp,
                Email = empresa.Email,
                Dominio = empresa.Dominio,
                EnderecoDoLogotipo = empresa.EnderecoDoLogotipo,
                EnderecoDaCapaDeFundo = empresa.EnderecoDaCapaDeFundo,
                StatusDeFuncionamento = empresa.StatusDeFuncionamento,
                HorariosDeFuncionamento = empresa.HorariosDeFuncionamento.Select(h => new HorarioFuncionamentoQueryDTO
                {
                    Codigo = h.Codigo,
                    Hora = h.Hora,
                    Minutos = h.Minutos,
                    DiaDaSemana = h.DiaDaSemana

                }).ToList()
            };
        }

        public static Empresa MapToEntity(this EmpresaCommandDTO empresaDTO)
        {
            return new Empresa
            {
                NomeFantasia = empresaDTO.NomeFantasia,
                RazaoSocial = empresaDTO.RazaoSocial,
                Cnpj = Regex.Replace(empresaDTO.Cnpj, "[^0-9]", ""),
                NumeroDoWhatsapp = empresaDTO.NumeroDoWhatsapp,
                Email = empresaDTO.Email,
                Dominio = empresaDTO.Dominio,
                EnderecoDoLogotipo = empresaDTO.EnderecoDoLogotipo,
                EnderecoDaCapaDeFundo = empresaDTO.EnderecoDaCapaDeFundo,
                StatusDeFuncionamento = empresaDTO.StatusDeFuncionamento,
                HorariosDeFuncionamento = empresaDTO.HorariosDeFuncionamento.Select(h => h.MapToEntity()).ToList()
            };
        }

        public static void MapUpdateEntity(this Empresa empresa, EmpresaCommandDTO empresaDTO)
        {
            empresa.NomeFantasia = empresaDTO.NomeFantasia;
            empresa.RazaoSocial = empresaDTO.RazaoSocial;
            empresa.Cnpj = Regex.Replace(empresaDTO.Cnpj, "[^0-9]", "");
            empresa.NumeroDoWhatsapp = empresaDTO.NumeroDoWhatsapp;
            empresa.Email = empresaDTO.Email;
            empresa.Dominio = empresaDTO.Dominio;
            empresa.EnderecoDoLogotipo = empresaDTO.EnderecoDoLogotipo;
            empresa.EnderecoDaCapaDeFundo = empresaDTO.EnderecoDaCapaDeFundo;
            empresa.StatusDeFuncionamento = empresaDTO.StatusDeFuncionamento;
            empresa.HorariosDeFuncionamento = empresaDTO.HorariosDeFuncionamento.Select(h => h.MapToEntity()).ToList();
        }
    }
}
