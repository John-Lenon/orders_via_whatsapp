using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Application.Interfaces.Utilities;
using Domain.Entities;
using Domain.Enumeradores.Empresa;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Application.Commands.Services
{
    public class ProdutoCommandService(IServiceProvider serviceProvider, IFileService _fileService)
        : CommandServiceBase<Produto, ProdutoCommandDTO, IProdutoRepository>(serviceProvider), IProdutoCommandService
    {
        protected override Produto MapToEntity(ProdutoCommandDTO entityDTO) =>
            entityDTO.MapToEntity();

        public async Task<bool> UploadImageAsync(ImageUploadRequestDto imageUpload)
        {
            if(!ValidateImageToUpoload(imageUpload)) return false;

            using var memoryStream = new MemoryStream();
            await imageUpload.File.CopyToAsync(memoryStream);

            string caminhoPasta = Path.Combine(imageUpload.Cnpj, imageUpload.TipoImagem.ToString());

            var success = await _fileService.SaveFileAsync(caminhoPasta,
                imageUpload.File.FileName,
                memoryStream.ToArray());

            if(!success) return false;

            return true;
        }

        public async Task<byte[]> GetImageAsync(ImageSearchRequestDto imageSearchRequest)
        {
            if(imageSearchRequest.Cnpj.IsNullOrEmpty() || imageSearchRequest.FileName.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente, "Cnpj ou Nome do arquivo não podem ser nulos ou vazios.");
                return null;
            }

            string caminhoPasta = Path.Combine(imageSearchRequest.Cnpj,
                imageSearchRequest.TipoImagem.ToString());

            var file = await _fileService.GetFileAsync(caminhoPasta, imageSearchRequest.FileName);

            if(file is null) return null;

            return file;
        }

        private bool ValidateImageToUpoload(ImageUploadRequestDto imageUpload)
        {
            bool isValid = true;

            if(imageUpload.Cnpj.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "Cnpj não pode ser nulo ou vazio.");

                isValid = false;
            }

            if(imageUpload.File == null || imageUpload.File.Length == 0)
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "O arquivo não pode ser nulo ou vazio.");

                isValid = false;
            }

            if(imageUpload.File.ContentType != "image/png" &&
               imageUpload.File.ContentType != "image/jpg")
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "A Imagem deve ser '.jpg' ou '.png'.");

                isValid = false;
            }

            return isValid;
        }
    }

    public class ImageUploadRequestDto
    {
        public string Cnpj { get; set; }
        public EnumTipoImagem TipoImagem { get; set; }
        public IFormFile File { get; set; }
    }

    public class ImageSearchRequestDto
    {
        public string Cnpj { get; set; }
        public EnumTipoImagem? TipoImagem { get; set; }
        public string FileName { get; set; }
    }
}
