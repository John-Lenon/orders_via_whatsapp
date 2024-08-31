using Application.Commands.DTO;
using Application.Commands.DTO.File;
using Application.Commands.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Interfaces
{
    public interface IEmpresaCommandService : ICommandServiceBase<EmpresaCommandDTO>
    {
        Task UpdateAsync(EmpresaCommandDTO entityDto, Guid codigo);

        Task<FileContentResult> GetCapaEmpresaAsync(string cnpj);
        Task<bool> UploadCapaEmpresaAsync(ImageUploadRequestDto imageUpload);

        Task<FileContentResult> GetLogoEmpresaAsync(string cnpj);
        Task<bool> UploadLogoEmpresaAsync(ImageUploadRequestDto imageUpload);
    }
}
