using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class BorderSideInfo
    {
        public ColorInfo Color { get; set; } = new ColorInfo();
        public bool Shadow { get; set; } = false;
        public int Space { get; set; } = 0;
        public int Size { get; set; } = 0;
        public BorderValues Value { get; set; } = BorderValues.Nil;

        public BorderSideInfo() { }
    }
}
