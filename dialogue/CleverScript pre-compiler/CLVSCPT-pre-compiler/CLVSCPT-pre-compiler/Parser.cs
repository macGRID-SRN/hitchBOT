using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CLVSCPT_pre_compiler
{
    class Parser
    {
        private string[] FileContents;

        public Parser(string fileName)
        {
            this.FileContents = File.ReadAllLines(fileName);
        }

        public Conversation PreCompile()
        {
            int count = 2;
            Conversation preCompiled = new Conversation(new Output(new List<string[]> { FileContents[1].Split('\t') }));
            List<Input> inputs = new List<Input>();
            List<Output> outputs = new List<Output>();

            List<ConversationNode> conversationNodes = new List<ConversationNode>();

            //probably could clean this up with reflection..
            do
            {
                int startingLine = count;
                string[] split = FileContents[count].Split('\t');
                if (split[0] == "input")
                {
                    inputs.Add(new Input(GetRelatedLines(ref count)));
                }
                else if (split[0] == "output")
                {
                    outputs.Add(new Output(GetRelatedLines(ref count)));
                }
                else if (split[0] == "phrase")
                {

                }
            }
            while (count < FileContents.Length);


            return preCompiled;
        }

        public List<CleverScriptLine> GetRelatedLines(ref int startingPoint)
        {
            List<CleverScriptLine> myLines = new List<CleverScriptLine>();
            do
            {
                myLines.Add(new CleverScriptLine(FileContents[startingPoint].Split('\t')));

                if (startingPoint++ >= FileContents.Length -1)
                {
                    break;
                }
            } while (string.IsNullOrWhiteSpace(FileContents[startingPoint].Split('\t')[0]));

            return myLines;
        }
    }
}
