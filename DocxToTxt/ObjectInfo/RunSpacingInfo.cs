using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class RunSpacingInfo
    {
        public int Spacing { get; set; } = 0;
        public int Width { get; set; } = 100;
        public int Kerning { get; set; } = -1;
        public FitTextInfo FitText { get; set; } = new FitTextInfo();
    }
}
