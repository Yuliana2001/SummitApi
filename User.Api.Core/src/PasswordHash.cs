using System;
namespace User.Api.Core{
    public class PasswordHash
    {
        public static string Generate(string password, string username)
        {
            var combined = password + username;
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(combined));
        }

        public static bool Validate(string password, User user, string PasswordHash)
        {
            return Generate(password, user.Username) == PasswordHash;
        }

    }
}