using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class PageMarginInfo
    {
        public int Bottom { get; set; } = 0;
        public int Footer { get; set; } = 0;
        public int Gutter { get; set; } = 0;
        public int Header { get; set; } = 0;
        public int Left { get; set; } = 0;
        public int Right { get; set; } = 0;
        public int Top { get; set; } = 0;

        public PageMarginInfo() { }
    }
}
