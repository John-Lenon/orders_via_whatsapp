using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Domain.Utilities
{
    public class PasswordHasher
    {
        public static (string Salt, string Hash) GerarSenhaHash(string senha)
        {
            byte[] codigoUnicoSenha = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(codigoUnicoSenha);
            }

            string hash = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: senha,
                    salt: codigoUnicoSenha,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 48
                )
            );

            return (Convert.ToBase64String(codigoUnicoSenha), hash);
        }

        public static string CompararSenhaHash(string senha, string codigoUnicoSenha)
        {
            byte[] codigoUnicoSenhaBytes = Convert.FromBase64String(codigoUnicoSenha);

            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: senha,
                    salt: codigoUnicoSenhaBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 48
                )
            );
        }
    }
}
