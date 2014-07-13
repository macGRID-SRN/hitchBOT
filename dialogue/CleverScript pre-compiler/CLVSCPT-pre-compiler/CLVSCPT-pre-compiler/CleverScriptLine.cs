using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class CleverScriptLine
    {
        string label;
        string description;
        string text;
        string gotoLabel;
        string ifs;
        string learn;
        string mode;
        int accuracy;

        public CleverScriptLine(string[] text)
        {
            this.label = text[1];
            this.description = text[2];
            this.text = text[3];
            this.ifs = text[4];
            this.learn = text[5];
            this.gotoLabel = text[6];
            if (!string.IsNullOrWhiteSpace(text[7]))
                this.accuracy = int.Parse(text[7]);
            else
                this.accuracy = 0;
            this.mode = text[8];
        }


    }
}
