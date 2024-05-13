namespace Domain.Entities.Usuario
{
    public class Usuario
    {
        public int Id { get; set; }
        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public string SenhaHash { get; set; }
        public string CodigoUnicoSenha { get; set; }

        public virtual ICollection<Permissao> Permissoes { get; set; } = [];
    }
}
