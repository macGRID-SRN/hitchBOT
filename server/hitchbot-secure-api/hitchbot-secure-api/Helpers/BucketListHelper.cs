using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using hitchbot_secure_api.Dal;
using hitchbot_secure_api.Helpers.Location;
using hitchbot_secure_api.Models;
using Microsoft.Ajax.Utilities;
using NetTopologySuite.Index.IntervalRTree;

namespace hitchbot_secure_api.Helpers
{
    public static class BucketListHelper
    {
        private static readonly int _numBucketList = 16;
        private static readonly int _numBucketPastList = 16;
        private static readonly int _numBucketFuture = 5;
        private static readonly int _numCurrentLocationList = 8;
        private static readonly string alpha = "abcdefghijklmnop";
        public static List<VariableValuePair> GetBucketList(DatabaseContext db, int hitchBotId, Models.Location location)
        {
            var entries = db.CleverscriptContents.Where(k => k.isBucketList && k.HitchBotId == hitchBotId)
                .Where(l => !l.TimeVisited.HasValue).Select(l => new CleverPolyDistance { Locations = l.PolgonVertices.Select(a => a.Location).ToList(), Clevertext = l.CleverText }).ToList();

            entries.ForEach(k =>
            {
                k.Distance = LocationHelper.GetDistance(LocationHelper.MakePolygon(k.Locations), location);
            });


            var content = entries.OrderBy(l => l.Distance).Select(l => l.CleverList.First()).Take(Math.Min(alpha.Length, _numBucketList));

            var iter = 0;

            return content.Select(l => new VariableValuePair
            {
                //probably not a good idea or thread safe!
                key = "bucket_list_" + alpha[iter++],
                value = l
            }).ToList();
        }

        public static List<VariableValuePair> GetBucketPastList(DatabaseContext db, int hitchBotId, Models.Location location)
        {
            var entries = db.CleverscriptContents.Where(k => k.isBucketList && k.HitchBotId == hitchBotId)
                .Where(l => l.TimeVisited.HasValue).Select(l => new CleverPolyDistance { Locations = l.PolgonVertices.Select(a => a.Location).ToList(), Clevertext = l.VisitedCleverText }).ToList();

            entries.ForEach(k =>
            {
                k.Distance = LocationHelper.GetDistance(LocationHelper.MakePolygon(k.Locations), location);
            });


            var content = entries.OrderBy(l => l.Distance).Select(l => l.CleverList.First()).Take(Math.Min(alpha.Length, _numBucketPastList));

            var iter = 0;

            return content.Select(l => new VariableValuePair
            {
                //probably not a good idea or thread safe!
                key = "bucket_past_" + alpha[iter++],
                value = l
            }).ToList();
        }

        public static List<VariableValuePair> GetBucketFutureList(DatabaseContext db, int hitchBotId, Models.Location location)
        {
            var entries = db.CleverscriptContents.Where(k => k.isBucketList && k.HitchBotId == hitchBotId)
                .Where(l => !l.TimeVisited.HasValue).Select(l => new CleverPolyDistance { Locations = l.PolgonVertices.Select(a => a.Location).ToList(), Clevertext = l.CleverText }).ToList();

            entries.ForEach(k =>
            {
                k.Distance = LocationHelper.GetDistance(LocationHelper.MakePolygon(k.Locations), location);
            });


            var content = entries.OrderBy(l => l.Distance).Select(l => l.CleverList.First()).Take(Math.Min(alpha.Length, _numBucketFuture));

            var iter = 0;

            return content.Select(l => new VariableValuePair
            {
                //probably not a good idea or thread safe!
                key = "bucket_future_" + alpha[iter++],
                value = l
            }).ToList();
        }

        public static List<VariableValuePair> GetContentList(DatabaseContext db, int hitchBotId, Models.Location location)
        {
            var entries = db.CleverscriptContents.Where(k => !k.isBucketList && k.HitchBotId == hitchBotId)
                .Select(l => new CleverPolyIntersection { Locations = l.PolgonVertices.Select(a => a.Location).ToList(), Clevertext = l.CleverText }).ToList();

            entries.ForEach(k =>
            {
                k.Intersects = LocationHelper.PointInPolygon(k.Locations, location);
            });

            var content = entries.Where(l => l.Intersects).SelectMany(l => l.CleverList).Shuffle().Take(Math.Min(alpha.Length, _numCurrentLocationList));

            var iter = 0;

            return content.Select(l => new VariableValuePair
            {
                key = "current_loc_" + alpha[iter++],
                value = l
            }).ToList();
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var rng = new Random();
            return source.Shuffle(rng);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        public class CleverPoly
        {
            public List<Models.Location> Locations { get; set; }
            public string Clevertext { get; set; }

            public List<string> CleverList
            {
                get { return Clevertext.Replace('\r', ' ').Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)).Shuffle().ToList(); }
            }
        }

        public class CleverPolyDistance : CleverPoly
        {
            public double Distance { get; set; }

        }

        public class CleverPolyIntersection : CleverPoly
        {
            public bool Intersects { get; set; }
        }



        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();

            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}