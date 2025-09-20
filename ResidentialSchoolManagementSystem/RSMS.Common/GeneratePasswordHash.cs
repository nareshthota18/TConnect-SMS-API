using System.Security.Cryptography;

namespace RSMS.Common
{
    public static class GeneratePasswordHash
    {
        public static (byte[] Hash, byte[] Salt) GetPasswordHash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA512);

            byte[] hash = pbkdf2.GetBytes(64); 

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, 100000, HashAlgorithmName.SHA512);
            byte[] computedHash = pbkdf2.GetBytes(64); 
            return computedHash.SequenceEqual(storedHash);
        }
    }
}

