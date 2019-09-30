using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.NumberingUtils
{
    public class NumberingInfo
    {
        public AbstractNum AbstractNum { get; set; }
        public NumberingInstance NumberingInstance { get; set; }
        public List<Level> Levels { get; set; } = new List<Level>();



        public NumberingInfo() { }
    }
}