using Application.Commands.DTO;
using Application.Commands.DTO.File;
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
    public class ProdutoController(
        IServiceProvider serviceProvider,
        IProdutoQueryService _produtoQueryService,
        IProdutoCommandService _produtoCommandService
    ) : MainController(serviceProvider)
    {
        [HttpGet]
        public async Task<IEnumerable<ProdutoQueryDTO>> GetAsync(
            [FromQuery] ProdutoFilterDTO filter
        )
        {
            return await _produtoQueryService.GetAsync(filter);
        }

        [HttpPost]
        public async Task AddAsync([FromBody] ProdutoCommandDTO produto)
        {
            await _produtoCommandService.InsertAsync(produto);
        }

        #region Image
        [HttpGet("get-image-produto")]
        public async Task<FileContentResult> GetProdutoImageAsync([FromQuery] ImageSearchRequestDto imageSearch)
        {
            return await _produtoCommandService.GetProdutoImageAsync(imageSearch);
        }

        [HttpPost("upload-image-produto")]
        public async Task<bool> UploadProdutoImageAsync([FromForm] ImageUploadRequestDto imageUpload)
        {
            return await _produtoCommandService.UploadProdutoImageAsync(imageUpload);
        }
        #endregion
    }
}
