using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Conversation
    {
        Output StartingOutput;
        public List<Input> AlwaysListening = new List<Input>(); //IM ALWAYS WATCHING YOU MIKE WISOWSKY -Close enough
        public List<ConversationNode> Nodes;

        public Conversation()
        {

        }

        public void SetStartingPoint(Output StartingPoint)
        {
            this.StartingOutput = StartingPoint;

        }

        public Conversation(Output startingPoint)
        {
            this.StartingOutput = startingPoint;
        }
    }
}
