using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser myParser = new Parser("hello.txt");
            myParser.PreCompile();
        }
    }
}
