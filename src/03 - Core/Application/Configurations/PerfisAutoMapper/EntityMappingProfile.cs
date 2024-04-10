using AutoMapper;
using Domain.DTOs.Usuario;
using Domain.Entities.Usuario;

namespace Application.Configurations.PerfisAutoMapper
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
