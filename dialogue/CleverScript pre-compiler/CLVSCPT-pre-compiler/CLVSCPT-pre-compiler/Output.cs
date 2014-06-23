using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Output : ConversationNode
    {
        public List<CleverScriptLine> outputLines;

        public List<Input> inputs = new List<Input>();

        public Output(List<string[]> text)
        {
            outputLines = new List<CleverScriptLine>();

            foreach (string[] myArray in text)
            {
                outputLines.Add(new CleverScriptLine(myArray));
            }
        }

        public Output(List<CleverScriptLine> text)
        {
            this.outputLines = text;
        }
    }
}
