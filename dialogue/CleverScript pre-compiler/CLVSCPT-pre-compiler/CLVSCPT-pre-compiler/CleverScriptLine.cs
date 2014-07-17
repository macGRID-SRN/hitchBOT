using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class CleverScriptLine
    {
        public string label;
        public string description;
        public string text;
        public string gotoLabel;
        public string ifs;
        public string learn;
        public string mode;
        public string type;
        public int accuracy;

        public CleverScriptLine(string[] text)
        {
            this.type = text[0];
            this.label = text[1];
            this.description = text[2];
            this.text = text[3];
            this.ifs = text[4];
            this.learn = text[5];
            this.gotoLabel = text[6];
            if (!string.IsNullOrWhiteSpace(text[7]))
                this.accuracy = int.Parse(text[7]);
            else
                this.accuracy = 75;
            this.mode = text[8];
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public string[] getBaseText()
        {
            return new string[]{
                this.type, this.label,this.description,this.text,this.ifs,this.learn,this.gotoLabel,this.accuracy.ToString(),this.mode
            };
        }
    }
}
