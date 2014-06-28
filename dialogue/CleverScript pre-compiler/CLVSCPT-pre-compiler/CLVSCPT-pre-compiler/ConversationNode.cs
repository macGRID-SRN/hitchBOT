using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class ConversationNode
    {
        public List<ConversationNode> IN;
        public List<ConversationNode> OUT;
        public List<CleverScriptLine> lines;


    }
}
