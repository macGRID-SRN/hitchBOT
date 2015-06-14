using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Models
{
    public class LoginAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        [Required]
        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }
    }
}
