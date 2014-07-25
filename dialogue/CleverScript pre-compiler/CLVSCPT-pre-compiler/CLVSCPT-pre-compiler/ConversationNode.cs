using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class ConversationNode
    {
        public List<Input> inputs;
        public List<Input> sortedInputs = new List<Input>();
        public Output output;
        public string lm = string.Empty;

        public ConversationNode(Output output, List<Input> inputs)
        {
            this.output = output;
            this.inputs = inputs;
        }

        public void SortInputs()
        {
            foreach (Input input in inputs)
            {
                string[] desc = input.text.Split('/');
                foreach (string split in desc)
                {
                    string[] temp = input.getBaseText();
                    temp[3] = split;
                    sortedInputs.Add(new Input(temp));
                }
            }
        }
    }
}
