using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace rename_images
{
    class Program
    {
        public static readonly string folder = @"*****PATH_TO_IMAGES*****";
        static void Main(string[] args)
        {
            Directory.EnumerateFiles(folder).ToList().ForEach(filename =>
            {
                var split = Path.GetFileName(filename.Substring(0, filename.Length - 4)).Split(new string[] { "-" }, StringSplitOptions.None);
                var takenTime = ConvertUtcToEst(DateTime.MinValue.AddTicks(long.Parse(split[1])));
                var newFileName = folder + "\\" + takenTime.ToString("s").Replace(':', '.') + "-" + split[0] + ".jpg";

                File.Move(filename, newFileName);
            });
        }

        public static DateTime ConvertUtcToEst(DateTime dateInUtc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateInUtc, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        }
    }
}
