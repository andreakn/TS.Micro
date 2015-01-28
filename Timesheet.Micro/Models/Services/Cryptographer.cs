using System;
using System.Security.Cryptography;
using System.Text;

namespace Timesheet.Micro.Models.Services
{
    public class Cryptographer : ICryptographer
    {
        public string CreateSalt()
        {
            const int size = 64;
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string ComputeHash(string valueToHash)
        {
            HashAlgorithm algorithm = SHA512.Create();
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));
            return Convert.ToBase64String(hash);
        }

        public string GetPasswordHash(string password, string salt)
        {
            return ComputeHash(password + salt);
        }
    }
}
