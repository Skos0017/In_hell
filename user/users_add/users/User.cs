using System;

namespace UserManagement
{
    public class User
    {
        private string login;
        private string password;
        private string firstName;
        private string lastName;
        private string middleName;
        private string phoneNumber;
        private string email;
        private int userId;
        private bool isOrganizer;
        private bool isPaid;

        public User(string login, string password, string firstName, string lastName, string middleName,
                    string phoneNumber, string email, int userId, bool isOrganizer, bool isPaid)
        {
            this.login = login;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.userId = userId;
            this.isOrganizer = isOrganizer;
            this.isPaid = isPaid;
        }

        public string GetLogin() => login;
        public string GetPassword() => password;
        public string GetFirstName() => firstName;
        public string GetLastName() => lastName;
        public string GetMiddleName() => middleName;
        public string GetPhoneNumber() => phoneNumber;
        public string GetEmail() => email;
        public int GetUser Id() => userId;
        public bool IsOrganizer() => isOrganizer;
        public bool IsPaid() => isPaid;

        public object ToDictionary()
        {
            return new
            {
                login,
                firstName,
                lastName,
                middleName,
                phoneNumber,
                email,
                userId,
                isOrganizer,
                isPaid
            };
        }
    }
}
