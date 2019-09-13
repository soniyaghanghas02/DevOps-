using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenAuthentication.Models;

namespace TokenAuthentication
{
    public class UserService
    {
        public Users ValidateUser(string email, string password)
        {
            // Here you can write the code to validate
            // User from database and return accordingly
            // To test we use dummy list here
            var userList = GetUserList();
            var user = userList.FirstOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }

        public List<Users> GetUserList()
        {
            List<Users> lstUsers = new List<Users>();
            lstUsers.Add(new Users
            {
                Name = "Soniya",
                Id = 1,
                Email = "Soniya2gmail.com",
                Password = "123456"

            });
            lstUsers.Add(new Users
            {
                Name = "Soniya1",
                Id = 12,
                Email = "Soniya12@gmail.com",
                Password = "123456"

            });
            return lstUsers;
            // Create the list of user and return           
        }
    }
}