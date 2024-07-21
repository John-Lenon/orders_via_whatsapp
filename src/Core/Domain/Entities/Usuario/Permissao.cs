using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Permissao : EntityBase
    {
        public string Descricao { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; } = [];
    }
}
