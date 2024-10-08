﻿using Application.Commands.DTO.File;
using Application.Commands.DTO.Produtos;
using Application.Commands.Interfaces;
using Application.Queries.DTO;
using Application.Queries.DTO.Produto;
using Application.Queries.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Atributos.Auth;
using Presentation.Base;
using Presentation.Configurations.Extensions;

namespace Presentation.V1
{
    [ApiController]
    [RouterController("produto")]
    [ApiVersion(ApiConfig.V1)]
    [AutorizationApi]
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
        public async Task<FileContentResult> GetProdutoImageAsync(string cnpj)
        {
            return await _produtoCommandService.GetProdutoImageAsync(cnpj);
        }

        [HttpPost("upload-image-produto")]
        public async Task<bool> UploadProdutoImageAsync([FromForm] ImageUploadRequestDto imageUpload)
        {
            return await _produtoCommandService.UploadProdutoImageAsync(imageUpload);
        }
        #endregion
    }
}
