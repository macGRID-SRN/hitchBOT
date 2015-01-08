using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbotAPI.Models
{
    public class MapMarker
    {
        public int ID { get; set; }
        public Location TargetLocation { get; set; }
        public virtual Project Project { get; set; }

        public Location HasBeenVisited { get; set; }

        //header and body could contain valid HTML
        public string HeaderText { get; set; }
        public string BodyText { get; set; }

        public DateTime TimeAdded { get; set; }
    }
}
