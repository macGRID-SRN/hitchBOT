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
        public List<Input> AlwaysListeningSorted = new List<Input>();
        public List<ConversationNode> Nodes;
        public List<Phrase> Phrases;

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

        public void SortInputs()
        {
            foreach (Input input in AlwaysListening)
            {
                string[] desc = input.text.Split('/');
                foreach (string split in desc)
                {
                    string[] temp = input.getBaseText();
                    temp[3] = split;
                    AlwaysListeningSorted.Add(new Input(temp));
                }
            }
        }
    }
}
