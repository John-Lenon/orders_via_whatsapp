using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public string SenhaHash { get; set; }
        public string CodigoUnicoSenha { get; set; }

        public ICollection<Permissao> Permissoes { get; set; } = [];
    }
}
