using Domain.Enumeradores.Empresas;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.Commands.DTO.File
{
    public class ImageUploadRequestDto
    {
        public string Cnpj { get; set; }
        public IFormFile File { get; set; }
        [JsonIgnore]
        public EnumTipoImagem TipoImagem { get; set; }
    }
}
