using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLVSCPT_pre_compiler
{
    interface CleverScriptLine
    {
        string label;
        string description;
        string text;
        string gotoLabel;
        string ifs;
        string learn;
        string mode;
    }
}
