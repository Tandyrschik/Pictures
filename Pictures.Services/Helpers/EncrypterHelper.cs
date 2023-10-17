
using System.Security.Cryptography;

namespace Pictures.Domain.Helpers
{
    public class EncrypterHelper // ширование пароля для безопасности данных методом Key Derivation Function
    {
        public static string Encrypt(string password)
        {
            byte[] salt;
            byte[] buffer;

            using (Rfc2898DeriveBytes bytes = new(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
