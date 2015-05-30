using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public double? Accuracy { get; set; }
        public double? Velocity { get; set; }

        public LocationProvider LocationProvider { get; set; }

        public string NearestCity { get; set; }
        public bool ForceProduction { get; set; } /*force this point into being used in production*/

        public int? HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public DateTime? TakenTime { get; set; }
        public DateTime TimeAdded { get; set; }

        public Location()
        {
            TimeAdded = DateTime.UtcNow;
        }

        public Location(Controllers.ReturnObjects.GenericHitchBot context)
            : this()
        {
            HitchBotId = context.HitchBotId;
            TakenTime = context.DateTime;
        }
    }

    /// <summary>
    /// Where did a GPS point come from? It is always good to know!
    /// </summary>
    public enum LocationProvider
    {
        Unknown,
        ManualInsert, /*Sometimes a point is added as the map can get confusing at times if weird things happen.*/
        TabletAGPS,
        SpotGPS
    }
}
