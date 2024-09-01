using Application.Commands.DTO;
using Application.Commands.DTO.Enderecos;
using Application.Queries.DTO;
using Application.Queries.DTO.Enderecos;
using Application.Utilities;
using Domain.Entities.Empresas;
using Domain.Entities.Enderecos;
using System.Text.RegularExpressions;

namespace Application.Configurations.MappingsApp
{
    public static class EmpresaMap
    {
        public static EmpresaQueryDTO MapToDTO(this Empresa empresa)
        {
            var dto = empresa.MapToDTO<Empresa, EmpresaQueryDTO>();
            dto.HorariosDeFuncionamento = empresa.HorariosDeFuncionamento.Select(h => h.MapToDTO<HorarioFuncionamento, HorarioFuncionamentoQueryDTO>());
            dto.Endereco = empresa.Endereco?.MapToDTO<Endereco, EnderecoQueryDTO>();
            return dto;
        }

        public static Empresa MapToEntity(this EmpresaCommandDTO empresaDTO)
        {
            var entidade = empresaDTO.MapToEntity<EmpresaCommandDTO, Empresa>();
            entidade.Cnpj = Regex.Replace(entidade.Cnpj, "[^0-9]", "");
            entidade.HorariosDeFuncionamento = empresaDTO.HorariosDeFuncionamento.Select(h => h.MapToEntity<HorarioFuncionamentoCommandDTO, HorarioFuncionamento>()).ToList();
            return entidade;
        }

        public static void MapUpdateEntity(this Empresa empresa, EmpresaCommandDTO empresaDTO)
        {
            empresa.GetValuesFrom(empresaDTO);
            empresa.Cnpj = Regex.Replace(empresaDTO.Cnpj, "[^0-9]", "");
            empresa.HorariosDeFuncionamento = empresaDTO.HorariosDeFuncionamento?.Select(h => h.MapToEntity<HorarioFuncionamentoCommandDTO, HorarioFuncionamento>()).ToList();
            empresa.Endereco = empresaDTO.Endereco?.MapToDTO<EnderecoCommandDTO, Endereco>();
        }
    }
}
