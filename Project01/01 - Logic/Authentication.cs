using System.Security.Cryptography;
using System.Text;

namespace TBG.Logic;

public static class Authentication
{
    static int SaltSize = 32;
    static int HashSize = 64;
    readonly static string secretKey = File.ReadAllText(@"C:\Tools\key.txt");
    readonly static byte[] Pepper = Encoding.UTF8.GetBytes(secretKey);

    public static (byte[] Salt, byte[] Hash) HashPassword(string password)
    {
        byte[] salt = GenerateSalt();
        byte[] hash = HashWithSaltAndPepper(password, salt, Pepper);
        return (salt, hash);
    }
    static byte[] GenerateSalt()
    {
        byte[] salt = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private static byte[] HashWithSaltAndPepper(string password, byte[] salt, byte[] pepper)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combined = new byte[passwordBytes.Length + salt.Length + pepper.Length];

            Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, combined, passwordBytes.Length, salt.Length);
            Buffer.BlockCopy(pepper, 0, combined, passwordBytes.Length + salt.Length, pepper.Length);

            return sha256.ComputeHash(combined);
        }
    }

    public static bool VerifyPassword(byte[] salt, byte[] hash, string password)
    {
        byte[] computedHash = HashWithSaltAndPepper(password, salt, Pepper);
        return CryptographicOperations.FixedTimeEquals(computedHash, hash);
    }
}