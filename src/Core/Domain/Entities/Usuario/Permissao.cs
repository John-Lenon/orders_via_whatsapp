namespace Domain.Entities.Usuario
{
    public class Permissao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; } = [];
    }
}
