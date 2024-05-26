using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Permissao : EntityBase
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; } = [];
    }
}
