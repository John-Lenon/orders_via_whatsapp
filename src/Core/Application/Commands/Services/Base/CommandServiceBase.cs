using Application.Commands.Interfaces.Base;
using Domain.Entities.Base;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories.Base;
using Domain.Interfaces.Utilities;
using Domain.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commands.Services.Base
{
    public abstract class CommandServiceBase<TEntity, TEntityDTO, TIRepository>(IServiceProvider service) :
        ICommandServiceBase<TEntityDTO>
        where TEntity : EntityBase
        where TIRepository : class, IRepositoryBase<TEntity>
    {
        private readonly INotificador _notificador = service.GetService<INotificador>();
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

        protected abstract TEntity MapToEntity(TEntityDTO entityDTO);

        #region Protected Methods 

        protected void Notificar(EnumTipoNotificacao tipo, string message) =>
            _notificador.Add(new Notificacao(tipo, message));

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
                    Notificar(EnumTipoNotificacao.Informacao, $"{failure.Errors}");
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
    }
}
