using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories.Base;
using Domain.Interfaces.Utilities;
using Domain.Utilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commands.Services.Base
{
    public abstract class CommandServiceBase<TEntity, TIRepository>(IServiceProvider service)
        where TEntity : class, new()
        where TIRepository : class, IRepositoryBase<TEntity>
    {
        private readonly INotificador _notificador = service.GetService<INotificador>();
        protected readonly TIRepository _repository = service.GetService<TIRepository>();
        protected readonly HttpContext _httpContext = service
            .GetService<IHttpContextAccessor>()?
            .HttpContext;

        protected async Task<bool> CommitAsync()
        {
            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Falha na Operação.");
                return false;
            }
            return true;
        }

        protected void Notificar(EnumTipoNotificacao tipo, string message) =>
            _notificador.Add(new Notificacao(tipo, message));

        protected bool Validator<TEntityDto>(TEntityDto entityDto)
        {
            var validator = service.GetService<IValidator<TEntityDto>>();

            ValidationResult results = validator.Validate(entityDto);

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
    }
}
