using Domain.Entities.Base;
using Domain.Entities.Empresas;

namespace Domain.Entities.Enderecos
{
    public class Endereco : EntityBase
    {
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public int NumeroLogradouro { get; set; }
        public string Complemento { get; set; }

        public Empresa Empresa { get; set; }
    }
}
