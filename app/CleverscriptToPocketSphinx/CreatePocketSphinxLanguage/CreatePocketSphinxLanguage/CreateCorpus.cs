using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CreatePocketSphinxLanguage
{
    public class CreateCorpus
    {
        List<string> inputSentences = new List<string>();
        string path;
        public CreateCorpus(string path)
        {
            this.path = path;
        }

        public void getInput(string line)
        {
            string[] inputs;
            string[] row = line.Split(',');
            if (row.GetValue(0).Equals("input"))
            {
              inputs = row.GetValue(3).ToString().Split('/');
              foreach(string inputValue in inputs)
            {
                inputSentences.Add(inputValue);
            }
            }
        }

        public void makeCorpus()
        {
            if (inputSentences.Count > 0)
            {
                StreamWriter file = new StreamWriter(path);
    foreach(string item in inputSentences)
    {
        file.WriteLine(item);
    }
    file.Close();
            }
        }
    }
}
