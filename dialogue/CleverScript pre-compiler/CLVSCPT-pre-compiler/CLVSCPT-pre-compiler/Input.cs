using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Input : ConversationNode
    {
        public Input(List<CleverScriptLine> inputLines)
        {
           this.lines = inputLines;
        }
    }
}
