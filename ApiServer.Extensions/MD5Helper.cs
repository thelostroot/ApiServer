using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiServer.Extensions
{
    public sealed class MD5Helper
    {
        public static string CreateHash(string data)
        {
            byte[] hash = Encoding.ASCII.GetBytes(data);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}
