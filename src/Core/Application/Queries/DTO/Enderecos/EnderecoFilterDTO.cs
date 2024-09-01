using Application.Queries.DTO.Base;

namespace Application.Queries.DTO.Enderecos
{
    public class EnderecoFilterDTO : FilterBaseDTO
    {
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public int? NumeroLogradouro { get; set; }
    }
}
