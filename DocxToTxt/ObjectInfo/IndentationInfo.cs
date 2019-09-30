using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class IndentationInfo
    {
        public int Start { get; set; } = 0;
        public int End { get; set; } = 0;
        public int Hanging { get; set; } = 0;
        public int FirstLine { get; set; } = 0;

        public IndentationInfo() { }
    }
}
