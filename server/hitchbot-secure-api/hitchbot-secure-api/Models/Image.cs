using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }
        public DateTime TimeTaken { get; set; }
        public DateTime? TimeApproved { get; set; }
        public DateTime? TimeDenied { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
