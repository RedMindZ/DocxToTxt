using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class ShadingInfo
    {
        public ColorInfo Fill { get; set; } = new ColorInfo();
        public ColorInfo Color { get; set; } = new ColorInfo();
        public ShadingPatternValues Value { get; set; } = ShadingPatternValues.Nil;

        public ShadingInfo() { }
    }
}
