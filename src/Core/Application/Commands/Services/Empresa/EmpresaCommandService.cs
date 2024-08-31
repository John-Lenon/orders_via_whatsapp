using Application.Commands.DTO;
using Application.Commands.DTO.File;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Application.Resources.Messages;
using Domain.Entities.Empresa;
using Domain.Enumeradores.Empresa;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Commands.Services
{
    public class EmpresaCommandService(IServiceProvider serviceProvider)
        : CommandServiceBase<Empresa, EmpresaCommandDTO, IEmpresaRepository>(serviceProvider), IEmpresaCommandService
    {
        protected override Empresa MapToEntity(EmpresaCommandDTO entityDTO) =>
            entityDTO.MapToEntity();

        public async Task UpdateAsync(EmpresaCommandDTO entityDto, Guid codigo)
        {
            if (!Validator(entityDto)) return;

            var empresa = await _repository.Get()
                .Include(e => e.HorariosDeFuncionamento)
                .FirstOrDefaultAsync(e => e.Codigo == codigo);

            if (empresa is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.EmpresaNaoEncontrado);
                return;
            }

            empresa.MapUpdateEntity(entityDto);

            _repository.Update(empresa);
            await _repository.SaveChangesAsync();
        }

        #region Image
        public async Task<FileContentResult> GetCapaEmpresaAsync(string cnpj)
        {
            byte[] imgBytes = [];

            var filePath = await ObterCaminhoDeCapaFundoAsync(cnpj);

            if (filePath.IsNullOrEmpty())
            {
                return new FileContentResult(imgBytes, "image/jpeg");
            }

            imgBytes = await GetImageAsync(filePath);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<bool> UploadCapaEmpresaAsync(ImageUploadRequestDto imageUpload)
        {
            var (success, caminhoPasta) = await UploadImageAsync(imageUpload);

            if (success)
            {
                await SalvarCaminhoDeCapaFundoAsync(imageUpload.Cnpj, caminhoPasta);

            }
            return success;
        }

        public async Task<FileContentResult> GetLogoEmpresaAsync(string cnpj)
        {
            byte[] imgBytes = [];

            var filePath = await ObterCaminhoDeLogoAsync(cnpj);

            if (filePath.IsNullOrEmpty())
            {
                return new FileContentResult(imgBytes, "image/jpeg");
            }

            imgBytes = await GetImageAsync(filePath);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<bool> UploadLogoEmpresaAsync(ImageUploadRequestDto imageUpload)
        {
            imageUpload.TipoImagem = EnumTipoImagem.Logo;

            var (success, caminhoPasta) = await UploadImageAsync(imageUpload);

            if (success)
            {
                await SalvarCaminhoDeLogoAsync(imageUpload.Cnpj, caminhoPasta);
            }

            return success;
        }
        #endregion


        #region Suppport Methods

        public async Task<string> ObterCaminhoDeCapaFundoAsync(string cnpj)
        {
            if (cnpj.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.CnpjInvalido);

                return "";
            }

            var empresa = await _repository.Get(empresa => empresa.Cnpj == cnpj).FirstOrDefaultAsync();

            var filePath = empresa?.EnderecoDaCapaDeFundo;

            if (filePath.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.CapaFundoNaoEncontrada);
            }

            return filePath;
        }

        public async Task<string> ObterCaminhoDeLogoAsync(string cnpj)
        {
            if (cnpj.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.CnpjInvalido);

                return "";
            }

            var empresa = await _repository.Get(empresa => empresa.Cnpj == cnpj).FirstOrDefaultAsync();

            var filePath = empresa?.EnderecoDoLogotipo;

            if (filePath.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.LogoNaoEncontrada);
            }

            return filePath;
        }

        public async Task SalvarCaminhoDeCapaFundoAsync(string cnpj, string caminhoPasta)
        {
            var empresa = await _repository.Get(empresa => empresa.Cnpj == cnpj).FirstOrDefaultAsync();

            empresa.EnderecoDaCapaDeFundo = caminhoPasta;

            _repository.Update(empresa);
            await _repository.SaveChangesAsync();
        }

        public async Task SalvarCaminhoDeLogoAsync(string cnpj, string caminhoPasta)
        {
            var empresa = await _repository.Get(empresa => empresa.Cnpj == cnpj).FirstOrDefaultAsync();

            empresa.EnderecoDoLogotipo = caminhoPasta;

            _repository.Update(empresa);
            await _repository.SaveChangesAsync();
        }

        #endregion
    }
}
