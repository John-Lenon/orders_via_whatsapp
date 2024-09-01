using Application.Queries.DTO.Enderecos;
using Application.Queries.Interfaces.Enderecos;
using Application.Queries.Services.Base;
using Domain.Entities.Enderecos;
using Domain.Interfaces.Repositories.Enderecos;
using System.Linq.Expressions;

namespace Application.Queries.Services.Enderecos
{
    public class EnderecoQueryService(IServiceProvider service) :
        QueryServiceBase<IEnderecoRepository, EnderecoFilterDTO,
            EnderecoQueryDTO, Endereco>(service), IEnderecoQueryService
    {
        protected override Expression<Func<Endereco, bool>> GetFilterExpression(EnderecoFilterDTO filter)
        {
            if (filter == null) return x => true;
            return (endereco =>
                            (filter.Cep == null || filter.Cep == endereco.Cep)
                         && (filter.Bairro == null || filter.Bairro == endereco.Bairro)
                         && (filter.NumeroLogradouro == null || filter.NumeroLogradouro == endereco.NumeroLogradouro)
                         && (filter.Cidade == null || filter.Cidade == endereco.Cidade)
                         && (filter.Uf == null || filter.Uf == endereco.Uf));
        }
    }
}
