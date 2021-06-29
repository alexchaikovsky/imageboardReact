using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ImageBoardReact.Authentiication.Security
{
    public class PasswordManager
    {
        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic 
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }
        public static (string salt, string hash) GetSaltAndHash(string password)
        {
            string salt = CreateSalt(6);
            string saltedPassword = salt + password;

            // Create a new instance of the hash crypto service provider.
            string base64 = GetPasswordHash(saltedPassword);
            return (salt: salt, hash: base64);
        }

        private static string GetPasswordHash(string saltedPassword)
        {
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
            // Compute the Hash. This returns an array of Bytes.
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            // Optionally, represent the hash value as a base64-encoded string, 
            // For example, if you need to display the value or transmit it over a network.
            string base64 = Convert.ToBase64String(bytHash);
            return base64;
        }

        public static string GetHash(string salt, string password)
        {
            return GetPasswordHash(salt + password);
        }
    }
}
