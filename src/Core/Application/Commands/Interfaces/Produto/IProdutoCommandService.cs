using Application.Commands.DTO;
using Application.Commands.DTO.File;
using Application.Commands.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Interfaces
{
    public interface IProdutoCommandService : ICommandServiceBase<ProdutoCommandDTO>
    {
        Task<FileContentResult> GetProdutoImageAsync(ImageSearchRequestDto imageSearch);
        Task<bool> UploadProdutoImageAsync(ImageUploadRequestDto imageUpload);
    }
}
