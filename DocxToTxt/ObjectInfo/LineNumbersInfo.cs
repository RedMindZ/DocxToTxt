using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class LineNumbersInfo
    {
        public int CountBy { get; set; } = 1;
        public int Start { get; set; } = 1;
        public int Distance { get; set; } = 0;
        public LineNumberRestartValues Restart { get; set; } = LineNumberRestartValues.NewPage;

        public LineNumbersInfo() { }
    }
}
