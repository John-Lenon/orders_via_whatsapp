using Application.Commands.DTO.Produtos;
using Application.Commands.Interfaces.Produtos;
using Application.Queries.DTO.Produtos;
using Application.Queries.Interfaces.Produtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Atributos.Auth;
using Presentation.Base;
using Presentation.Configurations.Extensions;

namespace Presentation.V1.Produtos
{
    [ApiController]
    [RouterController("categoria-produto")]
    [ApiVersion(ApiConfig.V1)]
    [AutorizationApi]
    public class CategoriaProdutoController(IServiceProvider serviceProvider,
            ICategoriaProdutoCommandService _categoriaCommandService,
            ICategoriaProdutoQueryService _categoriaQueryService) : MainController(serviceProvider)
    {
        [HttpGet]
        public async Task<IEnumerable<CategoriaProdutoQueryDTO>> GetAsync(
            [FromQuery] CategoriaProdutoFilterDTO filter
        )
        {
            return await _categoriaQueryService.GetAsync(filter);
        }

        [HttpPost]
        public async Task AddAsync([FromBody] CategoriaProdutoCommandDTO produto)
        {
            await _categoriaCommandService.InsertAsync(produto);
        }
    }
}
