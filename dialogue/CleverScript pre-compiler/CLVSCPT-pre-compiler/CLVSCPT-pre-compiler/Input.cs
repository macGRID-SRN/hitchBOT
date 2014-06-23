using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Input : ConversationNode
    {
        public List<CleverScriptLine> inputLines;
        public List<Output> outputs = new List<Output>();

        public Input(List<CleverScriptLine> inputLines)
        {
            this.inputLines = inputLines;
        }
    }
}
