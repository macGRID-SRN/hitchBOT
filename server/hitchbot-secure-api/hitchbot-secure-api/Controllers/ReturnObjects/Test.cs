using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hitchbot_secure_api.Controllers.ReturnObjects;
using Newtonsoft.Json;

namespace hitchbot_secure_api.Controllers
{
    public partial class SystemController
    {
        public class ReturnTest : GenericHitchBot
        {
            public string Dec { get; set; }

            public int? _Dec
            {

                get
                {
                    if (string.IsNullOrWhiteSpace(Dec))
                        return null;
                    int temp;
                    if (int.TryParse(Dec, out temp))
                        return temp;
                    return null;
                }
            }
        }
    }
}
