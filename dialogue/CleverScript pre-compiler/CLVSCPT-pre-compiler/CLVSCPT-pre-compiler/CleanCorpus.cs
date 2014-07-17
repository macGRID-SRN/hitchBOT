using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CLVSCPT_pre_compiler
{
    public static class CleanCorpus
    {

        public static void removeSpecialCharacters(List<string> corpus)
        {
            string[] specialCharacters = new string[]
            {
                ",",
                ".",
                "?",
                "!",
                "-",
                "'",
                "\""
            };
            foreach(string line in corpus)
            {
                foreach(string sC in specialCharacters)
                {
                    if(line.Contains(sC))
                    {
                        line.Replace(sC, " ");
                    }
                }
            }
            outputCorpusFile(corpus);
        }

        private static void outputCorpusFile(List<string> corpusList)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string line in corpusList)
            {
                sb.Append(line + "\n");
            }
            using(StreamWriter fsW = new StreamWriter(Environment.SpecialFolder.Desktop + "corpus.txt"))
            {
                fsW.Write(sb.ToString());
            }
        }
    }
}
