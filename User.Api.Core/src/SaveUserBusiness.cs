using System;
namespace User.Api.Core{
    public class SaveUserBusiness
    {
        public SaveUserBusiness(User user){

        }
    public ServiceState Process(string firstName, string lastName, string email, string password, string username, DateTime? birthday){
           var generatedPassword = PasswordHash.Generate(password, username);
           try {
               MemoryDatabase.Instance.GetJson(username);
               return ServiceState.Rejected;
           } catch (Exception) {
            System.Console.WriteLine("User not found, can be created");
           }
           var user = new User
           {
               FirstName = firstName,
               LastName = lastName,
               Email = email,
               Username = username,
               Password = generatedPassword,
               Birthday = birthday
           };
           MemoryDatabase.Instance.ExecuteNonQuery(user);
           return ServiceState.Accepted;
        }
    }
}