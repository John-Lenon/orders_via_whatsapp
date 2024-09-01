using Application.Commands.DTO.File;
using Application.Commands.DTO.Produtos;
using Application.Commands.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Interfaces
{
    public interface IProdutoCommandService : ICommandServiceBase<ProdutoCommandDTO>
    {
        Task<FileContentResult> GetProdutoImageAsync(string cnpj);
        Task<bool> UploadProdutoImageAsync(ImageUploadRequestDto imageUpload);
    }
}
