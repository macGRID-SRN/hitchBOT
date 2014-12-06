using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace hitchbotAPI.Models
{
    public class Password
    {
        public int ID { get; set; }
        [JsonIgnore]
        public string Username { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string Hash { get; set; }
        public virtual hitchBOT hitchBOT { get; set; }

        public virtual ICollection<Face> Faces { get; set; } 
    }

    public static class PasswordHandler
    {
        public static string GetHash(string password)
        {
            SHA256 mySHA256 = SHA256Managed.Create();

            byte[] decoded = Encoding.UTF8.GetBytes(password);
            byte[] hashed = mySHA256.ComputeHash(decoded);
            string hashedPassword = string.Empty;

            foreach (byte hexdigit in hashed)
            {
                hashedPassword += hexdigit.ToString("X2", CultureInfo.InvariantCulture.NumberFormat);
            }

            return hashedPassword;
        }
    }
}