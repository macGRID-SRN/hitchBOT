using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Phrase : CleverScriptLine
    {

        public Phrase(string[] text, string phraseText)
            : base(text)
        {
            if (phraseText.Contains("->"))
            {
                var keep = phraseText.IndexOf("->");
                this.text = phraseText.Substring(0, keep);
            }
            else
                this.text = phraseText;
            this.type = "phrase";
        }
    }
}
