using EventsListBL.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EventsListBL.Services
{
    public class EncryptService : IEncryptService
    {
        public string GetEncryptedPassword(string password)
        {
            byte[] bytes = Encoding.Default.GetBytes(password);
            SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoServiceProvider.ComputeHash(bytes));
            return hash;
        }
    }
}
