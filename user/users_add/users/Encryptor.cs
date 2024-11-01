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

        // ����������� �� ���������
        public Encryptor()
        {
            // ��������� ����� � IV, ���� ��� �� ������
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

        // ����� ��� ��������� ����� � IV
        public void SetKeyAndIV(byte[] key, byte[] iv)
        {
            if (key.Length != 32) // AES-256 ������� 32 ����� �����
            {
                throw new ArgumentException("���� ������ ���� 32 ����� ��� AES-256.");
            }
            if (iv.Length != 16) // AES ������� 16 ���� IV
            {
                throw new ArgumentException("IV ������ ���� 16 ���� ��� AES.");
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

        // ����� ��� �������� ����� � IV �� �����
        public void LoadKeyAndIV(string keyFilePath, string ivFilePath)
        {
            key = File.ReadAllBytes(keyFilePath);
            iv = File.ReadAllBytes(ivFilePath);
        }

        // ����� ��� ���������� ����� � IV � ����
        public void SaveKeyAndIV(string keyFilePath, string ivFilePath)
        {
            File.WriteAllBytes(keyFilePath, key);
            File.WriteAllBytes(ivFilePath, iv);
        }
    }
}
    