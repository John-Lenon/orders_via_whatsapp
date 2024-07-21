using Application.Queries.DTO;
using Application.Queries.DTO.Usuario;
using Application.Queries.Interfaces.Base;
using Domain.Enumeradores.Pemissoes;

namespace Application.Queries.Interfaces.Usuario
{
    public interface IUsuarioQueryService : IQueryServiceBase<UsuarioFilterDTO, UsuarioQueryDTO>
    {
        bool PossuiPermissao(params EnumPermissoes[] permissoesNecessarias);
    }
}
