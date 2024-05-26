namespace Domain.DTOs
{
    public class UsuarioTokenCommandDTO
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}
