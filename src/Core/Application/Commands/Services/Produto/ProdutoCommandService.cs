using Application.Commands.DTO;
using Application.Commands.DTO.File;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Domain.Entities;
using Domain.Enumeradores.Empresa;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Services
{
    public class ProdutoCommandService(IServiceProvider serviceProvider)
        : CommandServiceBase<Produto, ProdutoCommandDTO, IProdutoRepository>(serviceProvider), IProdutoCommandService
    {
        protected override Produto MapToEntity(ProdutoCommandDTO entityDTO) =>
            entityDTO.MapToEntity();

        #region Image
        public async Task<FileContentResult> GetProdutoImageAsync(ImageSearchRequestDto imageSearch)
        {
            imageSearch.TipoImagem = EnumTipoImagem.Produto;

            var imgBytes = await GetImageAsync(imageSearch);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<bool> UploadProdutoImageAsync(ImageUploadRequestDto imageUpload)
        {
            imageUpload.TipoImagem = EnumTipoImagem.Produto;

            return await UploadImageAsync(imageUpload);
        }
        #endregion
    }
}
