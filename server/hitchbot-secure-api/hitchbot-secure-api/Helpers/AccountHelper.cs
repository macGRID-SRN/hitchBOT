using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCrypto;

namespace hitchbot_secure_api.Helpers
{
    public static class AccountHelper
    {
        public static bool VerifyPassword(string password, string salt, string hash)
        {
            ICryptoService service = new PBKDF2();
            string hashed = service.Compute(password, salt);

            return service.Compare(hash, hashed);
        }

        public static SaltHashPair GetNewHashAndSalt(string password)
        {
            ICryptoService service = new PBKDF2();

            var salt = service.GenerateSalt();

            var hash = service.Compute(password);

            return new SaltHashPair
            {
                Salt = salt,
                Hash = hash
            };
        }

        public class SaltHashPair
        {
            public string Salt { get; set; }
            public string Hash { get; set; }
        }
    }
}
