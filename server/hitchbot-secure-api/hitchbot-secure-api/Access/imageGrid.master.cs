using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hitchbot_secure_api.Dal;

namespace hitchbot_secure_api.Access
{
    public partial class imageGrid : System.Web.UI.MasterPage
    {
        private List<Models.Image> _images;
        private int skipNumber = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                int hitchBotId = (int)Session[SessionInfo.HitchBotId];
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        public void SetImageSkip(int skip)
        {
            skipNumber = skip;
        }

        public void SetImages(List<Models.Image> images)
        {
            _images = images;
        }

        public List<Models.Image> GetImages()
        {
            return _images;
        }

        public int GetSkip()
        {
            return skipNumber;
        }

        protected void OnClickDelete(object sender, EventArgs e)
        {
            var button = (Button)sender;

            var imageId = int.Parse(button.CommandArgument);

            using (var db = new DatabaseContext())
            {
                var image = db.Images.First(l => l.Id == imageId);
                image.TimeDenied = DateTime.UtcNow;
                db.SaveChanges();
            }
        }

        protected void OnClickMove(object sender, EventArgs e)
        {
            var path = new UriBuilder(Request.Url) {Query = string.Empty}.Uri;
            Response.Redirect(path + "?skip=" + (skipNumber + 50));
        }

        protected void OnClickSave(object sender, EventArgs e)
        {
            var button = (Button)sender;

            var imageId = int.Parse(button.CommandArgument);

            using (var db = new DatabaseContext())
            {
                var image = db.Images.First(l => l.Id == imageId);
                image.TimeApproved = DateTime.UtcNow;
                db.SaveChanges();
            }
        }
    }

    public static class MyExtensionMethods
    {
        private static readonly string _thumborEndpoint = ConfigurationManager.AppSettings["ThumborEndpoint"];
        public static string GetEstReadable(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")).ToString("F");
        }

        public static string GetDelayFromNow(this DateTime date)
        {
            return FormatTimeSpan(DateTime.UtcNow - date) + " ago";
        }

        public static string GetDelayFromNow(this DateTime? date)
        {
            if (date.HasValue)
            {
                var notNull = date ?? DateTime.Now;
                return FormatTimeSpan(DateTime.UtcNow - notNull) + " ago";
            }
            return "Unknown";
        }

        public static string GetDelayFromThen(this DateTime date1, DateTime now)
        {
            return FormatTimeSpan(now - date1);
        }

        public static string GetThumbnailLink(this string url)
        {
            return string.Format("{0}/400x300/smart/{1}", _thumborEndpoint, url).GetRotatedLink();
        }

        public static string GetRotatedLink(this string url)
        {
            return string.Format("{0}/filters:rotate(270)/{1}", _thumborEndpoint, url);
        }

        public static string FormatTimeSpan(TimeSpan span)
        {
            return string.Format("{0:%h} hour(s) {0:%m} minute(s)", span);
        }
    }
}