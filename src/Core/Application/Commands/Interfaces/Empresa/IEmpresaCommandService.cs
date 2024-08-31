using Application.Commands.DTO;
using Application.Commands.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Interfaces
{
    public interface IEmpresaCommandService : ICommandServiceBase<EmpresaCommandDTO>
    {
        Task UpdateAsync(EmpresaCommandDTO entityDto, Guid codigo);

        Task<FileContentResult> GetCapaEmpresaAsync(string cnpj);
        Task<bool> UploadCapaEmpresaAsync(string cnpj, IFormFile file);

        Task<FileContentResult> GetLogoEmpresaAsync(string cnpj);
        Task<bool> UploadLogoEmpresaAsync(string cnpj, IFormFile file);
    }
}
