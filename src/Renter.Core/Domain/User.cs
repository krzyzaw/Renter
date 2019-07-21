using System;
using System.Text.RegularExpressions;

namespace Renter.Core.Domain
{
    public class User
    {
        private static readonly Regex NameRegex = new Regex(@"^(?=[A-Za-z0-9])(?!.*[._()\[\]-]{2})[A-Za-z0-9._()\[\]-]{3,15}$");

        public Guid Id { get; protected set; }

        public string Email { get; protected set; }

        public string Username { get; protected set; }

        public string Password { get; protected set; }

        public string Salt { get; protected set; }

        public string FullName { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        public string Role { get; protected set; }

        protected User()
        {
        }

        public User(Guid userId, string email, string username,
            string password, string salt, string role)
        {
            Id = userId;
            SetEmail(email);
            SetUsername(username);
            SetPassword(password, salt);
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new Exception("Username is invalid.");
            }

            if (String.IsNullOrEmpty(username))
            {
                throw new Exception("Username is invalid.");
            }

            Username = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }


        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email can not be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new Exception("Salt can not be empty.");
            }
            if (password.Length < 4)
            {
                throw new Exception("Password must contain at least 4 characters.");
            }
            if (Password == password)
            {
                return;
            }

            Password = password;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}