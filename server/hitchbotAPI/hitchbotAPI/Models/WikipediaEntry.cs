using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbotAPI.Models
{
    public class WikipediaEntry
    {
        public int ID { get; set; }
        public Location TargetLocation { get; set; }
        public virtual hitchBOT HitchBot { get; set; }

        //it is likely that there will be more than one entry here.
        public string WikipediaText { get; set; }
        public double? RadiusKM { get; set; }

        public DateTime TimeAdded { get; set; }
    }
}
