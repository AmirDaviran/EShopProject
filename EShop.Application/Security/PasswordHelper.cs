using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BCrypt.Net;

namespace EShop.Application.Security
{
    public static class PasswordHelper
    {
        // Method to hash a password  
        public static string HashPassword(string password)
        {
            // Generate a salt and hash the password  
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Method to verify a password against the stored hash  
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Verify the password entered by the user matches the stored hash  
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
