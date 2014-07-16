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
            Conversation preCompiled = new Conversation(new Output(FileContents[1].Split('\t')));

            do
            {
                preCompiled.AlwaysListening.Add(new Input(FileContents[count].Split('\t')));
                count++;
            } while (FileContents[count + 1].Split('\t')[0] != "output");

            List<ConversationNode> conversationNodes = new List<ConversationNode>();

            List<Input> inputs = null;
            Output nodeOutput = null;
            List<Phrase> Phrases = new List<Phrase>();

            count++;
            //probably could clean this up with reflection..
            string lastMatch = "output";
            do
            {
                string[] temp = FileContents[count].Split('\t');

                switch (string.IsNullOrWhiteSpace(temp[0]) ? lastMatch : temp[0])
                {
                    case "output":
                        if (nodeOutput != null)
                        {
                            conversationNodes.Add(new ConversationNode(nodeOutput, inputs));
                        }

                        nodeOutput = new Output(temp);
                        inputs = new List<Input>();
                        lastMatch = "output";
                        break;

                    case "input":
                        inputs.Add(new Input(temp));
                        lastMatch = "input";
                        break;

                    case "phrase":
                        Phrases.Add(new Phrase(temp));
                        break;
                }

                count++;
            }
            while (count < FileContents.Length);

            preCompiled.Nodes = conversationNodes;
            CreateVocab("test");
            return preCompiled;
        }

        public void CreateLanguageModel(string fileName)
        {
            CreateVocab(fileName);
            CreateIDNgram(fileName);
        }

        public void CreateIDNgram(string fileNameIn)
        {
            RunCommandLine(@"binary\text2idngram -vocab binary\sync" + fileNameIn + @".vocab -idngram binary\sync\" + fileNameIn + @".idngram < binary\" + fileNameIn + @".txt");
        }

        public void CreateVocab(string fileNameIn)
        {
            RunCommandLine(@"binary\text2wfreq < binary\" + fileNameIn + @".txt | binary\wfreq2vocab > binary\sync\" + fileNameIn + ".vocab");
        }

        public void RunCommandLine(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + command;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
    }
}
