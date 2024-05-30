using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Queries.DTO;
using Application.Queries.DTO.Produto;
using Application.Queries.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Base;
using Presentation.Configurations.Extensions;

namespace Presentation.V1
{
    [ApiController]
    [RouterController("produto")]
    [ApiVersion(ApiConfig.V1)]
    public class ProdutoController : MainController
    {
        private readonly IProdutoQueryService _produtoQueryService;
        private readonly IProdutoCommandService _produtoCommandService;

        public ProdutoController(IServiceProvider serviceProvider,
            IProdutoQueryService produtoQueryService,
            IProdutoCommandService produtoCommandService) : base(serviceProvider)
        {
            _produtoQueryService = produtoQueryService;
            _produtoCommandService = produtoCommandService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoQueryDTO>> GetAsync([FromQuery] ProdutoFilterDTO filter)
        {
            return await _produtoQueryService.GetAsync(filter);
        }

        [HttpPost]
        public async Task AddAsync([FromBody] ProdutoCommandDTO produto)
        {
            await _produtoCommandService.InsertAsync(produto);
        }
    }
}
