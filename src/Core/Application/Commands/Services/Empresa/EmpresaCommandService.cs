using Application.Commands.DTO;
using Application.Commands.DTO.File;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Application.Resources.Messages;
using Domain.Entities.Empresas;
using Domain.Enumeradores.Empresas;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
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

        public override async Task UpdateAsync(EmpresaCommandDTO entityDto, Guid? codigo = null, bool saveChange = true)
        {
            if (!Validator(entityDto)) return;

            var empresa = await _repository.Get(empresa => empresa.Codigo == codigo)
                .Include(e => e.HorariosDeFuncionamento)
                .FirstOrDefaultAsync();

            if (empresa is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.EmpresaNaoEncontrada);
                return;
            }

            empresa.MapUpdateEntity(entityDto);

            _repository.Update(empresa);
            await _repository.SaveChangesAsync();
        }

        #region Image
        public async Task<FileContentResult> GetCapaEmpresaAsync()
        {
            byte[] imgBytes = [];

            var filePath = await ObterCaminhoDeCapaFundoAsync();

            if (filePath.IsNullOrEmpty())
            {
                return new FileContentResult(imgBytes, "image/jpeg");
            }

            imgBytes = await GetImageAsync(filePath);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<FileContentResult> GetLogoEmpresaAsync()
        {
            byte[] imgBytes = [];

            var filePath = await ObterCaminhoDeLogoAsync();

            if (filePath.IsNullOrEmpty())
            {
                return new FileContentResult(imgBytes, "image/jpeg");
            }

            imgBytes = await GetImageAsync(filePath);
            return new FileContentResult(imgBytes, "image/jpeg");
        }

        public async Task<bool> UploadCapaEmpresaAsync(IFormFile file)
        {
            var empresa = await _repository.Get().FirstOrDefaultAsync();

            if (empresa == null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.EmpresaNaoEncontrada);
                return false;
            }

            var imageUpload = new ImageUploadRequestDto
            {
                Cnpj = empresa.Cnpj,
                File = file,
                TipoImagem = EnumTipoImagem.Capa
            };

            var (success, caminhoPasta) = await UploadImageAsync(imageUpload);

            if (success)
            {
                await SalvarCaminhoDeCapaFundoAsync(empresa, caminhoPasta);

            }

            return success;
        }

        public async Task<bool> UploadLogoEmpresaAsync(IFormFile file)
        {
            var empresa = await _repository.Get().FirstOrDefaultAsync();

            if (empresa == null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.EmpresaNaoEncontrada);
                return false;
            }

            var imageUpload = new ImageUploadRequestDto
            {
                Cnpj = empresa.Cnpj,
                File = file,
                TipoImagem = EnumTipoImagem.Logo
            };

            var (success, caminhoPasta) = await UploadImageAsync(imageUpload);

            if (success)
            {
                await SalvarCaminhoDeLogoAsync(empresa, caminhoPasta);
            }

            return success;
        }
        #endregion


        #region Support Methods

        public async Task<string> ObterCaminhoDeCapaFundoAsync()
        {
            var empresa = await _repository.Get().FirstOrDefaultAsync();

            if (empresa == null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.EmpresaNaoEncontrada);
                return "";
            }

            var filePath = empresa.EnderecoDaCapaDeFundo;

            if (filePath.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.Informacao, Message.CapaFundoNaoEncontrada);
                return "";
            }

            return filePath;
        }

        public async Task<string> ObterCaminhoDeLogoAsync()
        {
            var empresa = await _repository.Get().FirstOrDefaultAsync();

            if (empresa == null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.EmpresaNaoEncontrada);
                return "";
            }

            var filePath = empresa.EnderecoDoLogotipo;

            if (filePath.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.Informacao, Message.LogoNaoEncontrada);
            }

            return filePath;
        }

        public async Task SalvarCaminhoDeCapaFundoAsync(Empresa empresa, string caminhoPasta)
        {
            empresa.EnderecoDaCapaDeFundo = caminhoPasta;

            _repository.Update(empresa);
            await _repository.SaveChangesAsync();
        }

        public async Task SalvarCaminhoDeLogoAsync(Empresa empresa, string caminhoPasta)
        {
            empresa.EnderecoDoLogotipo = caminhoPasta;

            _repository.Update(empresa);
            await _repository.SaveChangesAsync();
        }

        #endregion
    }
}
