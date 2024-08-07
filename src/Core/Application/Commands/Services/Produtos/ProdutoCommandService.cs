using Application.Commands.DTO.File;
using Application.Commands.DTO.Produtos;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Domain.Entities.Produtos;
using Domain.Enumeradores.Empresa;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.Services.Produtos
{
    public class ProdutoCommandService(
        IServiceProvider serviceProvider,
        ICategoriaProdutoRepository _categoriaRepository)
        : CommandServiceBase<Produto, ProdutoCommandDTO, IProdutoRepository>(serviceProvider), IProdutoCommandService
    {
        private readonly ICategoriaProdutoRepository _produtoRepository;

        protected override Produto MapToEntity(ProdutoCommandDTO entityDTO) =>
            entityDTO.MapToEntity();


        public override async Task InsertAsync(ProdutoCommandDTO entityDTO, bool saveChanges = true)
        {
            if (!Validator(entityDTO)) return;

            var categoria = await GetCategoriaProdutoAsync(entityDTO.CodigoCategoria);
            if (categoria == null) return;

            var entity = MapToEntity(entityDTO);
            entity.CategoriaId = categoria.Id;

            await _repository.InsertAsync(entity);
            if (saveChanges) await CommitAsync();
        }

        private async Task<CategoriaProduto> GetCategoriaProdutoAsync(Guid? codigo)
        {
            var categoria = await _categoriaRepository.GetByCodigoAsync(codigo);
            if (categoria == null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, "A categoria informada não foi localizada.");
                return null;
            }
            return categoria;
        }

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
