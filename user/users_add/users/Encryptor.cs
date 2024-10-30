using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UserManagement
{
    public class Encryptor
    {
        private byte[] key;
        private byte[] iv;

        // Конструктор по умолчанию
        public Encryptor()
        {
            // Генерация ключа и IV, если они не заданы
            if (key == null || iv == null)
            {
                using (var aes = Aes.Create())
                {
                    aes.GenerateKey();
                    aes.GenerateIV();
                    key = aes.Key;
                    iv = aes.IV;
                }
            }
        }

        // Метод для установки ключа и IV
        public void SetKeyAndIV(byte[] key, byte[] iv)
        {
            if (key.Length != 32) // AES-256 требует 32 байта ключа
            {
                throw new ArgumentException("Ключ должен быть 32 байта для AES-256.");
            }
            if (iv.Length != 16) // AES требует 16 байт IV
            {
                throw new ArgumentException("IV должен быть 16 байт для AES.");
            }

            this.key = key;
            this.iv = iv;
        }

        public string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
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

        // Метод для загрузки ключа и IV из файла
        public void LoadKeyAndIV(string keyFilePath, string ivFilePath)
        {
            key = File.ReadAllBytes(keyFilePath);
            iv = File.ReadAllBytes(ivFilePath);
        }

        // Метод для сохранения ключа и IV в файл
        public void SaveKeyAndIV(string keyFilePath, string ivFilePath)
        {
            File.WriteAllBytes(keyFilePath, key);
            File.WriteAllBytes(ivFilePath, iv);
        }
    }
}
    