using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class TextAlignmentInfo
    {
        public VerticalTextAlignmentValues Value { get; set; } = VerticalTextAlignmentValues.Auto;

        public TextAlignmentInfo() { }
    }
}
