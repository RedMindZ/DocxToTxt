using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class ParagraphInfo
    {
        public TextFrameInfo TextFrameInfo { get; set; }
        public IndentationInfo IndentationInfo { get; set; }
        public JustificationInfo JustificationInfo { get; set; }
        public bool KeepLines { get; set; }
        public bool KeepNext { get; set; }
        // public NumberingInfo NumberingInfo { get; set; } // Not done
        public int OutlineLevel { get; set; }
        public ParagraphBorderInfo BorderInfo { get; set; }
        // pStyle
        public RunInfo RunInfo { get; set; }
        // >>> sectPr
        public ShadingInfo ShadingInfo { get; set; }
        public SpacingInfo SpacingInfo { get; set; }
        public TabsInfo TabsInfo { get; set; }
        public TextAlignmentInfo TextAlignmentInfo { get; set; }
    }
}
