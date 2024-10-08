﻿using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Queries.DTO;
using Application.Queries.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Atributos.Auth;
using Presentation.Base;
using Presentation.Configurations.Extensions;

namespace Presentation.V1
{
    [ApiController]
    [RouterController("empresa")]
    [ApiVersion(ApiConfig.V1)]
    [AutorizationApi]
    public class EmpresaController(IServiceProvider serviceProvider,
        IEmpresaQueryService _empresaQueryService,
        IEmpresaCommandService _empresaCommandService) : MainController(serviceProvider)
    {
        [HttpGet]
        public async Task<EmpresaQueryDTO> GetAsync([FromQuery] EmpresaFilterDTO filter)
        {
            return (await _empresaQueryService.GetAsync(filter)).FirstOrDefault();
        }

        [HttpPut("{codigo}")]
        public async Task UpdateAsync([FromBody] EmpresaCommandDTO empresa, [FromRoute] Guid codigo)
        {
            await _empresaCommandService.UpdateAsync(empresa, codigo);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid codeEmpresa)
        {
            await _empresaCommandService.DeleteAsync(codeEmpresa);
        }

        #region Image
        [HttpGet("get-capa-empresa")]
        public async Task<FileContentResult> GetCapaEmpresaAsync()
        {
            return await _empresaCommandService.GetCapaEmpresaAsync();
        }

        [HttpGet("get-logo-empresa")]
        public async Task<FileContentResult> GetLogoEmpresaAsync()
        {
            return await _empresaCommandService.GetLogoEmpresaAsync();
        }

        [HttpPost("upload-capa-empresa")]
        public async Task<bool> UploadCapaEmpresaAsync(IFormFile file)
        {
            return await _empresaCommandService.UploadCapaEmpresaAsync(file);
        }

        [HttpPost("upload-logo-empresa")]
        public async Task<bool> UploadLogoEmpresaAsync(IFormFile file)
        {
            return await _empresaCommandService.UploadLogoEmpresaAsync(file);
        }
        #endregion
    }
}
