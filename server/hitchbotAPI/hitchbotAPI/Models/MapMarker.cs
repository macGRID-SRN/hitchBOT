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

        public bool Active { get; set; }

        public Location TargetLocation { get; set; }
        public double RadiusKM { get; set; }

        public virtual Project Project { get; set; }

        public bool HasBeenVisited { get; set; }

        //header and body could contain valid HTML
        public string HeaderText { get; set; }
        public string BodyText { get; set; }
        public string HeaderTextGerman { get; set; }
        public string BodyTextGerman { get; set; }

        public DateTime TimeAdded { get; set; }
    }
}
