
using System.Security.Cryptography;
using System.Text;

namespace Pictures.Domain.Helpers
{
    public class EncrypterHelper // ширование пароля для безопасности данных
    {
        public static string Encrypt(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
