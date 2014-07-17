using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Phrase : CleverScriptLine
    {

        public Phrase(string[] text)
            : base(text)
        {
            this.type = "phrase";
        }

        public Phrase(string[] text, string phraseText)
            : base(text)
        {
            this.text = phraseText;
            this.type = "phrase";
        }
    }
}
