using Domain.Enumeradores.Empresa;
using System.Text.Json.Serialization;

namespace Application.Commands.DTO.File
{
    public class ImageSearchRequestDto
    {
        public string Cnpj { get; set; }
        public string FileName { get; set; }

        [JsonIgnore]
        public EnumTipoImagem TipoImagem { get; set; }
    }
}
