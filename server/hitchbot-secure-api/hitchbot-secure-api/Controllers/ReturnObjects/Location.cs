using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using hitchbot_secure_api.Controllers.ReturnObjects;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class LocationController
    {
        public class ReturnLocation : GenericHitchBot
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double? Altitude { get; set; }
            public double? Accuracy { get; set; }
            public double? Velocity { get; set; }

            public int? LocationProviderEnum { get; set; }

            public LocationProvider _LocationProvider
            {
                get { return ((LocationProvider)(LocationProviderEnum ?? 0)); }
            }
        }
    }
}
