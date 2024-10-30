using System;
using System.IO;
using System.Text.Json;
using UserManagement;

class Program
{
    static void Main()
    {
        // Создание пользователя
        var user = new User(
            login: "user123",
            password: "password123",
            firstName: "Иван",
            lastName: "Иванов",
            middleName: "Иванович",
            phoneNumber: "1234567890",
            email: "ivan@example.com",
            userId: 1,
            isOrganizer: true,
            isPaid: false
        );

        // Шифрование данных
        var encryptor = new Encryptor();
        var encryptedData = new
        {
            login = encryptor.Encrypt(user.GetLogin()),
            password = encryptor.Encrypt(user.GetPassword()),
            phoneNumber = encryptor.Encrypt(user.GetPhoneNumber()),
            email = encryptor.Encrypt(user.GetEmail()),
            firstName = user.GetFirstName(),
            lastName = user.GetLastName(),
            middleName = user.GetMiddleName(),
            userId = user.GetUser,
            isOrganizer = user.IsOrganizer(),
            isPaid = user.IsPaid()
        };

        // Сохранение в JSON файл
        var json = JsonSerializer.Serialize(encryptedData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("user_data.json", json);

        // Сохранение ключа и вектора инициализации
        var (key, encryptionKey, iv) = encryptor.GetEncryptionParameters();
        File.WriteAllText("encryption_key.txt", key);

        var authService = new AuthService();

        // Пример данных для входа (в реальном приложении эти данные будут получены из формы React)
        string login = "user123";
        string password = "password123";
        string phoneNumber = "1234567890";
        string email = "ivan@example.com";
        int userId = 1;

        // Попытка входа
        bool isLoggedIn = authService.Login(login, password, phoneNumber, email, userId);

        if (isLoggedIn)
        {
            Console.WriteLine("Успешный вход!");
            // Здесь можно направить пользователя на нужную форму
        }
        else
        {
            Console.WriteLine("Неверный логин или пароль.");
        }

    }

    private static object Id()
    {
        throw new NotImplementedException();
    }
}
