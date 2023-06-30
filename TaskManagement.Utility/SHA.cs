using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


namespace TaskManagement.Utility
{
    public static class SHA
    {
        private static readonly byte[] Key = new byte[32];
        private static readonly byte[] IV = new byte[16];


        public static string Encrypt(string password)
        {
            byte[] encrypted;
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using var msEncrypt = new MemoryStream();
                using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(password);
                }

                encrypted = msEncrypt.ToArray();
            }
            return Convert.ToBase64String(encrypted);
        }



        public static string Decrypt(string encryptedPassword)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedPassword);
            string plaintext = null;

            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using var msDecrypt = new MemoryStream(cipherText);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                plaintext = srDecrypt.ReadToEnd();
            }

            return plaintext;
        }

    }
}
