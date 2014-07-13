using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class ConversationNode
    {
        public List<Input> inputs;
        public Output output;
        public string lm = string.Empty;

        public ConversationNode(Output output, List<Input> inputs)
        {
            this.output = output;
            this.inputs = inputs;
        }
    }
}
