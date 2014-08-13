using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hitchbotAPI.Models
{
    public class ExceptionLog
    {
        public virtual int ID { get; set; }
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
        public virtual string Arguments { get; set; }
        public virtual string Method { get; set; }
        public virtual DateTime TimeOccured { get; set; }
        public virtual DateTime? TimeAdded { get; set; }
        public virtual string Data { get; set; }
        public virtual string Action { get; set; }
        public virtual hitchBOT HitchBOT { get; set; }
    }
}