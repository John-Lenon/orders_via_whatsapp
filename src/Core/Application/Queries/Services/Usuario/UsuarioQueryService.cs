using Application.Queries.DTO;
using Application.Queries.DTO.Usuario;
using Application.Queries.Interfaces.Usuario;
using Application.Queries.Services.Base;
using Domain.Entities;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.Queries.Services
{
    public class UsuarioQueryService :
        QueryServiceBase<IUsuarioRepository, UsuarioFilterDTO, UsuarioQueryDTO, Usuario>,
        IUsuarioQueryService
    {
        private readonly HttpContext _httpContext;
        public IEnumerable<string> Permissoes { get; }

        public UsuarioQueryService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _httpContext = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
            Permissoes = _httpContext?.User.Claims.Where(x => x.Type == "Permissao")
                                                   .Select(claim => claim.Value);


        }

        public bool PossuiPermissao(params EnumPermissoes[] permissoesNecessarias)
        {
            var possuiPermissao = permissoesNecessarias.All(permissaoNecessaria =>
                Permissoes.Any(permissao => permissao == permissaoNecessaria.ToString())
            );

            return possuiPermissao;
        }

        protected override Expression<Func<Usuario, bool>> GetFilterExpression(UsuarioFilterDTO filter)
        {

            throw new NotImplementedException();
        }

        protected override UsuarioQueryDTO MapToDTO(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
