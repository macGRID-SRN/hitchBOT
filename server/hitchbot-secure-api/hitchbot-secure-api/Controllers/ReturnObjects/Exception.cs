using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hitchbot_secure_api.Controllers.ReturnObjects;

namespace hitchbot_secure_api.Controllers
{
    public partial class ExceptionController
    {
        public class ReturnException : GenericHitchBot
        {
            public string Message { get; set; }
            public string Exception { get; set; }
            public string Arguments { get; set; }
            public string Method { get; set; }
            public string Data { get; set; }
            public string Action { get; set; }
        }
    }
}
