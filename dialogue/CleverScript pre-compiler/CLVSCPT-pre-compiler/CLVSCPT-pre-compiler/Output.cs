using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Output : ConversationNode
    {
        public Output(List<string[]> text)
        {
            this.lines = new List<CleverScriptLine>();

            foreach (string[] myArray in text)
            {
                lines.Add(new CleverScriptLine(myArray));
            }
        }

        public Output(List<CleverScriptLine> text)
        {
            this.lines = text;
        }
    }
}
