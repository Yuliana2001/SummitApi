using System;
namespace User.Api.Core{
    public class GetUserBusiness{
        public GetUserBusiness(User user){

        }
        public string Process(string username ){
              try {
                var userJson = Database.Instance.GetJson(username);
                return userJson;
              } catch (Exception) {
                System.Console.WriteLine("User not found");
                return null;
              }
        }
    }
}