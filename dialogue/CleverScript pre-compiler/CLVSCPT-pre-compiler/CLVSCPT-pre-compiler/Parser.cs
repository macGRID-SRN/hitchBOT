using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CLVSCPT_pre_compiler
{
    class Parser
    {
        private string[] FileContents;

        public Parser(string fileName)
        {
            this.FileContents = File.ReadAllLines(fileName);
        }

        public List<string> CorpusLines = new List<string>();

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
            List<Input> masterInputs = new List<Input>();
            List<Input> inputs = null;
            Output nodeOutput = null;
            Dictionary<string, List<Phrase>> PhraseLookup = new Dictionary<string, List<Phrase>>();

            count++;
            //probably could clean this up with reflection..
            string lastMatch = "output";
            string lastLabel = "";
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
                        lastMatch = "phrase";
                        if (!PhraseLookup.ContainsKey(string.IsNullOrWhiteSpace(temp[1]) ? lastLabel : temp[1]))
                        {
                            PhraseLookup.Add(temp[1], ParsePhrases(temp));
                        }
                        else
                        {
                            temp[1] = lastLabel;
                            PhraseLookup[lastLabel].AddRange(ParsePhrases(temp));
                        }
                        lastLabel = temp[1];
                        break;
                }

                count++;
            }
            while (count < FileContents.Length);

            foreach (ConversationNode conNode in conversationNodes)
            {
                conNode.SortInputs();
            }

            preCompiled.SortInputs();

            preCompiled.Nodes = conversationNodes;
            preCompiled.PhraseLookup = PhraseLookup;


            //CreateLanguageModel("test");
            return preCompiled;
        }

        public List<Phrase> ParsePhrases(string[] text)
        {
            List<Phrase> myPhrase = new List<Phrase>();

            foreach (string temp in text[3].Split('/'))
            {
                myPhrase.Add(new Phrase(text, temp));
            }

            return myPhrase;
        }

        public void BuildCorpus(Conversation conversation)
        {
            foreach (Input myInput in conversation.AlwaysListeningSorted)
            {
                Phrase2String(myInput.text, conversation.CorpusLines);
            }

            foreach (ConversationNode node in conversation.Nodes)
            {
                foreach (Input myInput in node.sortedInputs)
                {
                    Phrase2String(myInput.text, conversation.CorpusLines);
                }
            }
        }

        public void Phrase2String(string input, List<string> tempList)
        {
            if (ContainsOptionalText(input))
            {
                //remove and replace - recurse
            }
            else if (ContainsAnyPhrase(input))
            {
                //inject phrase and recurse

                if (ContainsOptionalPhrase(input))
                {
                    //remove phrase and recurse
                }
            }
            else
                //add new corpus line
                tempList.Add(input);

        }

        public bool ContainsAnyPhrase(string textInput)
        {
            Regex myRegex = new Regex(@"\(\((\!?\??)(.+?)\)\)");


            return myRegex.IsMatch(textInput);
        }

        public bool ContainsPhrase(string textInput)
        {
            Regex myRegex = new Regex(@"\(\((.+?)\)\)");


            return myRegex.IsMatch(textInput);
        }

        public bool ContainsOptionalText(string textInput)
        {
            Regex myRegex = new Regex(@"\((\?.+?)\)");


            return myRegex.IsMatch(textInput);
        }

        public bool ContainsOptionalPhrase(string textInput)
        {
            Regex myRegex = new Regex(@"\(\((\!?\?)(.+?)\)\)");


            return myRegex.IsMatch(textInput);
        }

        public void CreateLanguageModel(string fileName)
        {
            CreateVocab(fileName);
            CreateIDNgram(fileName);
            CreateARPA(fileName);
            CreateDMP(fileName);
            CreateHash(fileName);
        }

        public void CreateHash(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(@"binary\sync\" + fileName + ".DMP"))
                {
                    File.WriteAllText(@"binary\sync\" + fileName + ".md5", BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower());
                }
            }
        }

        public void CreateDMP(string fileNameIn)
        {
            RunCommandLine(@"binary\sphinx_lm_convert -i binary\sync\" + fileNameIn + @".arpa -o binary\sync\" + fileNameIn + @".DMP");
        }

        public void CreateIDNgram(string fileNameIn)
        {
            RunCommandLine(@"binary\text2idngram -vocab binary\sync\" + fileNameIn + @".vocab -idngram binary\sync\" + fileNameIn + @".idngram < binary\" + fileNameIn + @".txt");
        }

        public void CreateARPA(string fileNameIn)
        {
            RunCommandLine(@"binary\idngram2lm -vocab_type 0 -vocab binary\sync\" + fileNameIn + @".vocab -idngram binary\sync\" + fileNameIn + @".idngram -arpa binary\sync\" + fileNameIn + @".arpa");
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

        public string getPhrase(string phr)
        {
            string regexDoubleParen = @"\(\(([^]]*)\)\)";
            List<string> phrases = Regex.Matches(phr, regexDoubleParen).Cast<Match>().Select(x => x.Groups[1].Value).ToList();
            if(phrases[0].Contains("!") || phrases.Contains("?"))
            {
                phrases[0].Replace("!", "");
                phrases[0].Replace("?", "");
            }
            return phrases[0];

        }
        public string putPhrase(string originalPhrase, string originalWord, string replacement)
        {
            bool done = false;
            string[] termList = originalPhrase.Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach(string term in termList )
            {
                string testString = term.Replace("!", "");
                testString = testString.Replace("?", "");

                if(testString.Contains("((" + originalWord+"))") && !done )
                {
                    sb.Append(testString.Replace("((" + originalWord+"))", replacement));
                    done = true;
                }
                else
                {
                    sb.Append(term);
                }
            }
            return sb.ToString();

        }

        public string getPhraseSingle(string phr)
        {
            string regexDoubleParen = @"\(([^]]*)\)";
            List<string> phrases = Regex.Matches(phr, regexDoubleParen).Cast<Match>().Select(x => x.Groups[1].Value).ToList();
            if (phrases[0].Contains("!") || phrases.Contains("?"))
            {
                phrases[0].Replace("!", "");
                phrases[0].Replace("?", "");
            }
            return phrases[0];

        }
        public string putPhraseSingle(string originalPhrase, string originalWord, string replacement)
        {
            bool done = false;
            string[] termList = originalPhrase.Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach (string term in termList)
            {
                string testString = term.Replace("!", "");
                testString = testString.Replace("?", "");

                if (testString.Contains("(" + originalWord + ")") && !done)
                {
                    sb.Append(testString.Replace("(" + originalWord + ")", replacement));
                    done = true;
                }
                else
                {
                    sb.Append(term);
                }
            }
            return sb.ToString();

        }
    }
}
