using Application.Commands.DTO.File;
using Application.Commands.Interfaces.Base;
using Application.Utilities.Utilities;
using Domain.Entities.Base;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories.Base;
using Domain.Interfaces.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application.Commands.Services.Base
{
    public abstract class CommandServiceBase<TEntity, TEntityDTO, TIRepository>(IServiceProvider service) :
        ICommandServiceBase<TEntityDTO>
        where TEntity : EntityBase
        where TIRepository : class, IRepositoryBase<TEntity>
    {
        private readonly INotificador _notificador = service.GetService<INotificador>();
        private readonly IFileService _fileService = service.GetService<IFileService>();

        protected readonly TIRepository _repository = service.GetService<TIRepository>();
        protected readonly HttpContext _httpContext = service.GetService<IHttpContextAccessor>()?.HttpContext;
        protected readonly IValidator<TEntityDTO> validator = service.GetService<IValidator<TEntityDTO>>();

        public async Task DeleteAsync(Guid codigo, bool saveChanges = true)
        {
            var entityLocated = await _repository.Get(item => item.Codigo == codigo).FirstOrDefaultAsync();

            if (entityLocated == null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, "Entidade não encontrada para deleção");
                return;
            }

            _repository.Delete(entityLocated);
            if (saveChanges) await CommitAsync();
        }

        public virtual async Task InsertAsync(TEntityDTO entityDTO, bool saveChanges = true)
        {
            if (!Validator(entityDTO)) return;

            var entity = MapToEntity(entityDTO);
            await _repository.InsertAsync(entity);
            if (saveChanges) await CommitAsync();
        }

        public virtual async Task UpdateAsync(TEntityDTO entityDTO, bool saveChanges = true)
        {
            if (!Validator(entityDTO)) return;

            var entity = MapToEntity(entityDTO);
            _repository.Update(entity);
            if (saveChanges) await CommitAsync();
        }

        public virtual Task PatchAsync(TEntityDTO entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }



        #region Protected Methods 
        protected async Task<bool> UploadImageAsync(ImageUploadRequestDto imageUpload)
        {
            if (!ValidateImageToUpoload(imageUpload)) return false;

            using var memoryStream = new MemoryStream();
            await imageUpload.File.CopyToAsync(memoryStream);

            string caminhoPasta = Path.Combine(imageUpload.Cnpj, imageUpload.TipoImagem.ToString());

            var success = await _fileService.SaveFileAsync(caminhoPasta,
                imageUpload.File.FileName, memoryStream.ToArray());

            if (!success) return false;

            return true;
        }

        protected async Task<byte[]> GetImageAsync(ImageSearchRequestDto imageSearchRequest)
        {
            if (imageSearchRequest.Cnpj.IsNullOrEmpty() ||
                imageSearchRequest.FileName.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "Cnpj ou Nome do arquivo não podem ser nulos ou vazios.");

                return [];
            }

            string caminhoPasta = Path.Combine(imageSearchRequest.Cnpj,
                imageSearchRequest.TipoImagem.ToString());

            var file = await _fileService.GetFileAsync(caminhoPasta, imageSearchRequest.FileName);

            if (file is null) return [];

            return file;
        }

        protected abstract TEntity MapToEntity(TEntityDTO entityDTO);

        protected void Notificar(EnumTipoNotificacao tipo, string message) =>
            _notificador.Notify(tipo, message);

        protected bool Validator(TEntityDTO entityDto)
        {
            var results = validator.Validate(entityDto);

            if (!results.IsValid)
            {
                var groupedFailures = results
                    .Errors.GroupBy(failure => failure.PropertyName)
                    .Select(group => new
                    {
                        Errors = string.Join(" ", group.Select(err => err.ErrorMessage))
                    });

                foreach (var failure in groupedFailures)
                {
                    Notificar(EnumTipoNotificacao.ErroCliente, $"{failure.Errors}");
                }

                return false;
            }

            return true;
        }

        protected async Task<bool> CommitAsync()
        {
            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Falha na Operação.");
                return false;
            }
            return true;
        }
        #endregion

        #region Private Methods
        private bool ValidateImageToUpoload(ImageUploadRequestDto imageUpload)
        {
            bool isValid = true;

            if (imageUpload.Cnpj.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "Cnpj não pode ser nulo ou vazio.");

                isValid = false;
            }

            if (imageUpload.File == null || imageUpload.File.Length == 0)
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "O arquivo não pode ser nulo ou vazio.");

                isValid = false;
            }

            if (imageUpload.File.ContentType != "image/png" &&
               imageUpload.File.ContentType != "image/jpeg")
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "A Imagem deve ser '.jpg' ou '.png'.");

                isValid = false;
            }

            return isValid;
        }
        #endregion
    }
}
