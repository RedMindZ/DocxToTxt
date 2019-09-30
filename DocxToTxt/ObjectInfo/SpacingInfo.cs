using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class SpacingInfo
    {
        public int After { get; set; } = 0;
        public int Before { get; set; } = 0;
        public int Line { get; set; } = 0;
        public LineSpacingRuleValues LineRule { get; set; } = LineSpacingRuleValues.Auto;
        public bool AfterAutospacing { get; set; }
        public bool BeforeAutospacing { get; set; }

        public SpacingInfo() { }
    }
}
