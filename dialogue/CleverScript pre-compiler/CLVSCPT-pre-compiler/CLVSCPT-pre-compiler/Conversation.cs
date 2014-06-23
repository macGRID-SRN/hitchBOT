using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Conversation
    {
        Output StartingOutput;

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
