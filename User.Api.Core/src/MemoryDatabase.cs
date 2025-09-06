using System;
using System.Collections.Generic;
namespace User.Api.Core
{
    public class MemoryDatabase : Database
    {
        private static readonly Lazy<MemoryDatabase> lazy =
            new Lazy<MemoryDatabase>(() => new MemoryDatabase());

        public static MemoryDatabase Instance { get { return lazy.Value; } }

        private static List<User> UsersList = new List<User>();

        private MemoryDatabase()
        {
        }

        public override void ExecuteNonQuery(User user)
        {
            Database.Instance = this;
            
            if (UsersList.Any(u => u.Username == user.Username)){
                throw new Exception("Username already exists");
            }
            UsersList.Add(user);
        }

        public override String GetJson(String username = null)
        {
            if (string.IsNullOrEmpty(username))
            {
                return System.Text.Json.JsonSerializer.Serialize(UsersList);
            }
            var user = UsersList.Find(u => u.Username == username);
            if (user == null){
                throw new Exception("User not found");
            };
            return System.Text.Json.JsonSerializer.Serialize(user);
        }
    }
}