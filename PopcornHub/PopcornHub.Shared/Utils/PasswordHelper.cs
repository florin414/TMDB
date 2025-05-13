using System.Text;
using Org.BouncyCastle.Crypto.Generators;

namespace PopcornHub.Shared.Utils;

public static class PasswordHelper
{
    public static int Cost = 12;
    
    public static byte[] HashPassword(string password, byte[] salt, int cost = 12)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        return BCrypt.Generate(passwordBytes, salt, cost);
    }

    public static bool VerifyPassword(string inputPassword, string storedHashBase64, string storedSaltBase64, int cost = 12)
    {
        var inputBytes = Encoding.UTF8.GetBytes(inputPassword);
        var saltBytes = Convert.FromBase64String(storedSaltBase64);
        var storedHashBytes = Convert.FromBase64String(storedHashBase64);

        var computedHash = BCrypt.Generate(inputBytes, saltBytes, cost);
        return storedHashBytes.SequenceEqual(computedHash);
    }
}