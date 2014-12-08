using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace hitchbotAPI.Models
{
    /// <summary>
    /// represents a whole face for a hitchbot, a collection of any number of panels.
    /// </summary>
    public class Face
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        ///not virtual because we don't want lazy loading for panels.
        public ICollection<LedPanel> Panels { get; set; }

        public virtual Password UserAccount { get; set; }

        public DateTime TimeAdded { get; set; }
    }

    /// <summary>
    /// represents the each Panel, which has 24 bits wide x 16 bits tall.
    /// </summary>
    public class LedPanel
    {
        public int ID { get; set; }
        
        //should be 16 rows foreach LedPanel
        public ICollection<Row> Rows { get; set; }

        //You could add other panels after I guess.
        public DateTime TimeAdded { get; set; }
    }

    /// <summary>
    /// represents the row, 24 bits long
    /// </summary>
    public class Row
    {
        public int ID { get; set; }
        public byte ColSet0 { get; set; }
        public byte ColSet1 { get; set; }
        public byte ColSet2 { get; set; }
    }
}
