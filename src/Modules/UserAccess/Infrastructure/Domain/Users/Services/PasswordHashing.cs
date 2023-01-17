using System.Security.Cryptography;
using System.Text;

namespace Clean.Modules.UserAccess.Domain.Users.Services;
internal class PasswordHashing : IPasswordHashing
{
    public string Hash(string password)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }
}