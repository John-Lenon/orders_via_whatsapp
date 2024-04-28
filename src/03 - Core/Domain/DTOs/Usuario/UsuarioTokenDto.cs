namespace Domain.DTOs.Usuario
{
    public class UsuarioTokenDto
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}
