using System.Security.Cryptography;
using System.Text;

namespace RSMS.Common
{
    public static class GeneratePasswordHash
    {
        public static (byte[] Hash, byte[] Salt) GetPasswordHash(string password)
        {
            // ex : string password = "Super@123"; 

            using (var hmac = new HMACSHA512())
            {
                byte[] salt = hmac.Key; // HMAC key = salt
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return (hash, salt);
            }
        }
    }
}

