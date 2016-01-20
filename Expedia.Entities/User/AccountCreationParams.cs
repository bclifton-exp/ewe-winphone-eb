using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.User
{
    public class AccountCreationParams
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }      
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool ExpediaEmailOptIn { get; set; }

        public AccountCreationParams(string email, string password, string firstName, string middleName, string lastName,bool optIn)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            ExpediaEmailOptIn = optIn;
        }

        public AccountCreationParams(string email, string password, string firstName, string lastName, bool optIn)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            MiddleName = null;
            LastName = lastName;
            ExpediaEmailOptIn = optIn;
        }
    }
}
