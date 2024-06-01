using Application.Commands.DTO;
using Application.Commands.Interfaces.Base;
using Application.Commands.Services;

namespace Application.Commands.Interfaces
{
    public interface IProdutoCommandService : ICommandServiceBase<ProdutoCommandDTO>
    {
        Task<byte[]> GetImageAsync(ImageSearchRequestDto imageSearchRequestDto);
        Task<bool> UploadImageAsync(ImageUploadRequestDto imageUploadRequestDto);
    }
}
