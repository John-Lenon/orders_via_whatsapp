using Application.Commands.DTO;
using Application.Commands.DTO.File;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Domain.Entities.Empresa;
using Domain.Enumeradores.Empresa;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Services
{
    public class EmpresaCommandService(IServiceProvider serviceProvider)
        : CommandServiceBase<Empresa, EmpresaCommandDTO, IEmpresaRepository>(serviceProvider), IEmpresaCommandService
    {
        protected override Empresa MapToEntity(EmpresaCommandDTO entityDTO) =>
            entityDTO.MapToEntity();

        public async Task UpdateAsync(EmpresaCommandDTO entityDto, Guid codigo)
        {
            if(!Validator(entityDto)) return;

            var empresa = await _repository.Get()
                .Include(e => e.HorariosDeFuncionamento)
                .FirstOrDefaultAsync(e => e.Codigo == codigo);

            if(empresa is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, "Empresa não foi encontrada.");
                return;
            }

            empresa.MapUpdateEntity(entityDto);

            _repository.Update(empresa);
            await _repository.SaveChangesAsync();
        }

        #region Image
        public async Task<FileContentResult> GetCapaEmpresaAsync(ImageSearchRequestDto imageSearch)
        {
            imageSearch.TipoImagem = EnumTipoImagem.Capa;

            var imgBytes = await GetImageAsync(imageSearch);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<bool> UploadCapaEmpresaAsync(ImageUploadRequestDto imageUpload)
        {
            imageUpload.TipoImagem = EnumTipoImagem.Capa;

            return await UploadImageAsync(imageUpload);
        }

        public async Task<FileContentResult> GetLogoEmpresaAsync(ImageSearchRequestDto imageSearch)
        {
            imageSearch.TipoImagem = EnumTipoImagem.Logo;

            var imgBytes = await GetImageAsync(imageSearch);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<bool> UploadLogoEmpresaAsync(ImageUploadRequestDto imageUpload)
        {
            imageUpload.TipoImagem = EnumTipoImagem.Logo;

            return await UploadImageAsync(imageUpload);
        }
        #endregion
    }
}
