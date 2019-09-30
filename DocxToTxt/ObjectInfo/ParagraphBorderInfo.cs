using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class ParagraphBorderInfo
    {
        public BorderSideInfo Top { get; set; } = new BorderSideInfo();
        public BorderSideInfo Bottom { get; set; } = new BorderSideInfo();
        public BorderSideInfo Left { get; set; } = new BorderSideInfo();
        public BorderSideInfo Right { get; set; } = new BorderSideInfo();
        public BorderSideInfo Between { get; set; } = new BorderSideInfo();

        public ParagraphBorderInfo() { }
    }
}
