using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AirlinesNET.Utilities
{
    static class Shortcuts
    {
        public static int AuthUserID { get; set; }
        public static bool checkIfEmptyString(params string[] fields)
        {
            foreach(string field in fields)
            {
                if (String.IsNullOrEmpty(field))
                {
                    return true;
                }
            }
            return false;
        }

        public static string hashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

    }
}
