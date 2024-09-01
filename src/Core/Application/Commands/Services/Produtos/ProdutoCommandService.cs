using Application.Commands.DTO.File;
using Application.Commands.DTO.Produtos;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Resources.Messages;
using Domain.Entities.Produtos;
using Domain.Enumeradores.Empresas;
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
                Notificar(EnumTipoNotificacao.ErroCliente, Message.CategoriaNaoEncontrada);
                return null;
            }
            return categoria;
        }

        #region Image
        public async Task<FileContentResult> GetProdutoImageAsync(string cnpj)
        {
            var imgBytes = await GetImageAsync(cnpj);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<bool> UploadProdutoImageAsync(ImageUploadRequestDto imageUpload)
        {
            imageUpload.TipoImagem = EnumTipoImagem.Produto;

            var upload = await UploadImageAsync(imageUpload);

            return upload.Success;
        }
        #endregion
    }
}
