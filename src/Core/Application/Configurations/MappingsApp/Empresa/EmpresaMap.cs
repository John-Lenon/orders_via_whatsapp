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
            empresa.StatusDeFuncionamento = empresaDTO.StatusDeFuncionamento;
            empresa.EnderecoDaCapaDeFundo = UpdateCnpjInPath(empresa.EnderecoDaCapaDeFundo, empresa.Cnpj);
            empresa.EnderecoDoLogotipo = UpdateCnpjInPath(empresa.EnderecoDoLogotipo, empresa.Cnpj);
            empresa.HorariosDeFuncionamento = empresaDTO.HorariosDeFuncionamento.Select(h => h.MapToEntity()).ToList();
        }

        private static string UpdateCnpjInPath(string path, string newCnpj)
        {
            var pathSegments = path.Split('\\');
            if (pathSegments.Length > 1)
            {
                var cnpjAntigo = pathSegments[0];
                pathSegments[0] = newCnpj;

                var directoryAntigo = Path.Combine(AppSettings.CompanyFilePaths, cnpjAntigo);
                var newDirectory = Path.Combine(AppSettings.CompanyFilePaths, newCnpj);

                if (Directory.Exists(directoryAntigo) && !Directory.Exists(newDirectory))
                {
                    Directory.Move(directoryAntigo, newDirectory);
                }

                return string.Join("\\", pathSegments);
            }

            return path;
        }
    }
}
