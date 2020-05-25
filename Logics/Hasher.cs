using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logics
{
    public class Hasher
    {
        public string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public bool ValidatePassword(User user, string Password)
        {
            return BCrypt.Net.BCrypt.Verify(Password, user.password);
        }
    }
}
