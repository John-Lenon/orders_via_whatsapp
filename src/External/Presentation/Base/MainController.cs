using Application.Resources.Messages;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Utilities;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Base
{
    [ApiController]
    public abstract class MainController(IServiceProvider service) : Controller
    {
        private readonly INotificador _notifier = service.GetRequiredService<INotificador>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ValidarModelState(context))
                return;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.Result = context.Result switch
            {
                ObjectResult objectResult => CustomResponse(objectResult.Value),

                FileContentResult fileResult when fileResult.FileContents.Length > 0 => File(fileResult.FileContents, fileResult.ContentType),

                _ => CustomResponse<object>(null),
            };
        }

        private IActionResult CustomResponse<TResponse>(TResponse content)
        {
            if (_notifier.HasNotifications(EnumTipoNotificacao.ErroCliente, out var clientErrors))
            {
                return BadRequest(new ResponseResultDTO<TResponse>(content) { Mensagens = clientErrors });
            }

            if (_notifier.HasNotifications(EnumTipoNotificacao.ErroInterno, out var serverErrors))
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseResultDTO<TResponse>(content) { Mensagens = serverErrors });
            }

            _notifier.HasNotifications(EnumTipoNotificacao.Informacao, out var infoMessages);

            return Ok(new ResponseResultDTO<TResponse>(content) { Mensagens = infoMessages });
        }

        protected void NotificarErro(string mensagem) =>
            _notifier.Notify(EnumTipoNotificacao.ErroCliente, mensagem);

        private bool ValidarModelState(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                if (!ValidarContentTypeRequest(modelState, context))
                    return false;

                var valoresInvalidosModelState = modelState.Where(x =>
                    x.Value.ValidationState == ModelValidationState.Invalid
                );
                if (valoresInvalidosModelState.Count() == 0)
                    return true;

                ExtrairMensagensDeErroDaModelState(valoresInvalidosModelState, context);
                return false;
            }
            return true;
        }

        private void ExtrairMensagensDeErroDaModelState(
            IEnumerable<KeyValuePair<string, ModelStateEntry>> valoresInvalidosModelState,
            ActionExecutingContext context
        )
        {
            var listaErros = new List<Notificacao>();
            foreach (var model in valoresInvalidosModelState)
            {
                var nomeCampo = model.Key.StartsWith("$.") ? model.Key.Substring(2) : model.Key;
                listaErros.Add(
                    new Notificacao(
                        EnumTipoNotificacao.ErroCliente,
                        string.Format(Message.CampoFormatoInvalido, nomeCampo)
                    )
                );
            }

            context.Result = new BadRequestObjectResult(
                new ResponseResultDTO<string>(null, listaErros.ToArray())
            );
        }

        private bool ValidarContentTypeRequest(
            ModelStateDictionary modelState,
            ActionExecutingContext context
        )
        {
            var valoresInvalidosModelState = modelState.Where(x =>
                x.Value.ValidationState == ModelValidationState.Invalid
            );
            if (valoresInvalidosModelState.Count() == 1)
            {
                var model = valoresInvalidosModelState.First();
                var modelErro = model.Value.Errors.FirstOrDefault();

                if (model.Key == string.Empty && modelErro.ErrorMessage == string.Empty)
                {
                    var result = new ResponseResultDTO<object>();
                    result.ContentTypeInvalido();
                    context.Result = new BadRequestObjectResult(result);
                    return false;
                }
                else if (model.Key == string.Empty)
                {
                    model.Value.ValidationState = ModelValidationState.Valid;
                    return true;
                }
            }
            return true;
        }
    }

    public class ResponseResultDTO<TResponse>
    {
        public TResponse Dados { get; set; }
        public Notificacao[] Mensagens { get; set; }

        public ResponseResultDTO(TResponse data, Notificacao[] notificacoes = null)
        {
            Dados = data;
            Mensagens = notificacoes;
        }

        public void ContentTypeInvalido()
        {
            Mensagens = [new(EnumTipoNotificacao.ErroCliente, "Content-Type inválido.")];
        }

        public ResponseResultDTO() { }
    }
}
