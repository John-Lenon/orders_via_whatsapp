using Application.Commands.DTO.Enderecos;
using Application.Commands.Interfaces.Enderecos;
using Application.Commands.Services.Base;
using Application.Utilities;
using Domain.Entities.Enderecos;
using Domain.Interfaces.Repositories.Enderecos;

namespace Application.Commands.Services.Enderecos
{
    public class EnderecoCommandService(IServiceProvider serviceProvider)
        : CommandServiceBase<Endereco, EnderecoCommandDTO, IEnderecoRepository>(serviceProvider), IEnderecoCommandService
    {
        public async Task Teste(EnderecoCommandDTO endereco)
        {
            var entidade = endereco.MapToEntity<EnderecoCommandDTO, Endereco>();
        }
    }
}
