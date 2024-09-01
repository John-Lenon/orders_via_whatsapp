using Application.Commands.DTO.Enderecos;
using Application.Commands.Interfaces.Enderecos;
using Application.Queries.DTO.Enderecos;
using Application.Queries.Interfaces.Enderecos;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Atributos.Auth;
using Presentation.Base;
using Presentation.Configurations.Extensions;

namespace Presentation.V1.Enderecos
{
    [ApiController]
    [RouterController("endereco")]
    [ApiVersion(ApiConfig.V1)]
    [AutorizationApi]
    public class EnderecoController(
        IServiceProvider serviceProvider,
        IEnderecoCommandService _enderecoCommand,
        IEnderecoQueryService _enderecoQuery) : MainController(serviceProvider)
    {
        [HttpGet]
        public async Task<IEnumerable<EnderecoQueryDTO>> GetAsync([FromQuery] EnderecoFilterDTO filter)
        {
            return await _enderecoQuery.GetAsync(filter);
        }

        [HttpPut("{codigo}")]
        public async Task UpdateAsync([FromBody] EnderecoCommandDTO endereco, [FromRoute] Guid codigo)
        {
            await _enderecoCommand.UpdateAsync(endereco, codigo);
        }
    }
}
