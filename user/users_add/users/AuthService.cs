using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using UserManagement;

namespace UserManagement
{
    public class AuthService
    {
        private readonly string jsonFilePath = "user_data.json";
        private readonly Encryptor encryptor;

        public AuthService()
        {
            encryptor = new Encryptor();
        }

        public bool Login(string login, string password, string phoneNumber, int userId)
        {
            // Чтение данных из JSON файла
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("User  data file not found.");
            }

            var json = File.ReadAllText(jsonFilePath);
            var users = JsonSerializer.Deserialize<User[]>(json);

            // Поиск пользователя
            var user = users?.FirstOrDefault(u =>
                u.login == login &&
                u.userId == userId &&
                u.phoneNumber == phoneNumber);

            if (user == null)
            {
                return false; // Пользователь не найден
            }

            // Шифрование введенного пароля для сравнения
            var encryptedPassword = encryptor.Encrypt(password);

            // Проверка пароля
            if (encryptedPassword == user.GetPassword())
            {
                // Пользователь найден и пароль совпадает
                return true; // Успешный вход
            }

            return false; // Неверный пароль
        }
    }
}
