using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class CustomerLogin
    {
        public CustomerLogin() { }

        public CustomerLogin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
            //this.Password = LoginUtility.Password;
        }
        public int CustomerID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
