using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace AppointmentSite
{
    public class LoginManager
    {
        private readonly string masterUsername = "adminusername";
        private readonly string masterPassword = "749F09BADE8ACA755660EEB17792DA880218D4FBDC4E25FBEC279D7FE9F65D70";

        // Validates that the given username and password are correct
        public bool ValidateLogin(string username, string password)
        {
            // First check if username provided (non-case sensitive) is correct, then check if password is correct (case-sensitive)
            if (String.Equals(username, masterUsername, StringComparison.CurrentCultureIgnoreCase))
            {
                if (GetEncryptedPassword(password) == masterPassword)
                {
                    return true;
                }
            }
            return false;
        }

        // Returns the password given after SHA256 encryption
        private string GetEncryptedPassword(string plainPass)
        {
            using (var encryptor = SHA256.Create())
            {
                // Get the bytes from the SHA256 hash
                byte[] modifiedPlainPass = encryptor.ComputeHash(Encoding.UTF8.GetBytes(plainPass));

                StringBuilder builder = new StringBuilder();

                // Build up a string of hexadecimal characters represented by byte array
                foreach (byte part in modifiedPlainPass)
                {
                    builder.Append(part.ToString("X2"));
                }

                // Make StringBuilder into String
                var encryptedPass = builder.ToString();

                return encryptedPass;
            }
        }
    }
}
