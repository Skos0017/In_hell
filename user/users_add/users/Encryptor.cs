using System;
using System.Security.Cryptography;
using System.Text;

namespace UserManagement
{
    public class Encryptor
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public Encryptor()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                key = aes.Key;
                iv = aes.IV;
            }
        }

        public string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new System.IO.StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public (string encryptedData, byte[] key, byte[] iv) GetEncryptionParameters()
        {
            return (Convert.ToBase64String(key), key, iv);
        }
    }
}
