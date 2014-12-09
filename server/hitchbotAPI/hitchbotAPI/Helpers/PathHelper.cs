using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace hitchbotAPI.Helpers
{
    public static class PathHelper
    {
        public static string GetJsBuildPath()
        {
            //one hell of a namespace here.
            return System.Web.HttpContext.Current.Server.MapPath("~/JSbuild/");
        }
    }
}
