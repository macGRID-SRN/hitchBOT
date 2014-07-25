using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Input : CleverScriptLine
    {
        public Input(string[] text)
            : base(text)
        {
            this.type = "input";
        }

        public Input SortCopy()
        {
            Input other = (Input)this.MemberwiseClone();
            return other;
        }
    }
}
