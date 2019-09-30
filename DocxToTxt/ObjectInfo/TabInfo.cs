using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class TabInfo
    {
        public TabStopLeaderCharValues Leader { get; set; } = TabStopLeaderCharValues.None;
        public int Position { get; set; } = 0;
        public TabStopValues Value { get; set; } = TabStopValues.Clear;

        public TabInfo() { }
    }
}
