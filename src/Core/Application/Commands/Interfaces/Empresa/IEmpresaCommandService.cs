using Application.Commands.DTO;
using Application.Commands.DTO.File;
using Application.Commands.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Interfaces
{
    public interface IEmpresaCommandService : ICommandServiceBase<EmpresaCommandDTO>
    {
        Task<FileContentResult> GetCapaEmpresaAsync(ImageSearchRequestDto imageSearch);
        Task<bool> UploadCapaEmpresaAsync(ImageUploadRequestDto imageUpload);
        Task<FileContentResult> GetLogoEmpresaAsync(ImageSearchRequestDto imageSearch);
        Task<bool> UploadLogoEmpresaAsync(ImageUploadRequestDto imageUpload);
    }
}
