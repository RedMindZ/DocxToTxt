using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class UnderlineInfo
    {
        public ColorInfo Color { get; set; }
        public UnderlineValues Value { get; set; }

        public UnderlineInfo() { }
    }
}
