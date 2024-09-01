using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Queries.DTO;
using Application.Queries.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Atributos.Auth;
using Presentation.Base;
using Presentation.Configurations.Extensions;

namespace Presentation.V1.Empresa
{
    [ApiController]
    [RouterController("horario-fucionamento")]
    [ApiVersion(ApiConfig.V1)]
    [AutorizationApi]
    public class HorariosDeFuncionamentoController(
        IServiceProvider _serviceProvider,
        IHorarioFuncionamentoCommandService _horarioFuncionamentoCommand,
        IHorarioFuncionamentoQueryService _horarioFuncionamentoQuery) : MainController(_serviceProvider)
    {

        [HttpGet]
        public async Task<IEnumerable<HorarioFuncionamentoQueryDTO>> GetAsync([FromQuery] HorarioFuncionamentoFilterDTO filter)
        {
            return await _horarioFuncionamentoQuery.GetAsync(filter);
        }

        [HttpPost]
        public async Task AddAsync([FromBody] HorarioFuncionamentoCommandDTO empresa)
        {
            await _horarioFuncionamentoCommand.InsertAsync(empresa);
        }

        [HttpPut("{codigo}")]
        public async Task UpdateAsync([FromBody] HorarioFuncionamentoCommandDTO empresa, [FromRoute] Guid codigo)
        {
            await _horarioFuncionamentoCommand.UpdateAsync(empresa, codigo);
        }

        [HttpDelete("{codigo}")]
        public async Task DeleteAsync([FromRoute] Guid codeEmpresa)
        {
            await _horarioFuncionamentoCommand.DeleteAsync(codeEmpresa);
        }
    }
}
