using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Commands.Services;
using Application.Queries.DTO.Produto;
using Application.Queries.Interfaces.Produto;
using Asp.Versioning;
using Domain.Enumeradores.Empresa;
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

        [HttpPost("upload-logo")]
        public async Task<bool> UploadLogo([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            imageUploadRequestDto.TipoImagem = EnumTipoImagem.Logo;
            return await _produtoCommandService.UploadImageAsync(imageUploadRequestDto);
        }

        [HttpGet("get-logo")]
        public async Task<FileContentResult> GetLogo([FromQuery] ImageSearchRequestDto imageSearchRequestDto)
        {
            imageSearchRequestDto.TipoImagem = EnumTipoImagem.Produto;

            byte[] imageData = await _produtoCommandService.GetImageAsync(imageSearchRequestDto);

            if(imageData == null) return null;

            return File(imageData, "image/jpeg");
        }
    }
}
