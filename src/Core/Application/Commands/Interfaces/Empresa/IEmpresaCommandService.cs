using Application.Commands.DTO;
using Application.Commands.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Interfaces
{
    public interface IEmpresaCommandService : ICommandServiceBase<EmpresaCommandDTO>
    {
        Task<FileContentResult> GetCapaEmpresaAsync();
        Task<FileContentResult> GetLogoEmpresaAsync();
        Task<bool> UploadLogoEmpresaAsync(IFormFile file);
        Task<bool> UploadCapaEmpresaAsync(IFormFile file);
    }
}
