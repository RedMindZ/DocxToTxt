using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class TextFrameInfo
    {
        public bool AnchorLock { get; set; } = false;
        public HorizontalAnchorValues HorizontalAnchor { get; set; } = HorizontalAnchorValues.Text;
        public VerticalAnchorValues VerticalAnchor { get; set; } = VerticalAnchorValues.Text;

        public DropCapLocationValues DropCap { get; set; } = DropCapLocationValues.None;
        public int Lines { get; set; } = 1;

        public int Height { get; set; } = 0;
        public int Width { get; set; } = 0;

        public HeightRuleValues HeightRule { get; set; } = HeightRuleValues.Auto;

        public int HorizontalSpace { get; set; } = 0;
        public int VerticalSpace { get; set; } = 0;

        public TextWrappingValues Wrap { get; set; } = TextWrappingValues.Auto;

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public HorizontalAlignmentValues XAlignment { get; set; } = HorizontalAlignmentValues.Left;
        public VerticalAlignmentValues YAlignment { get; set; } = VerticalAlignmentValues.Inline;

        public TextFrameInfo() { }
    }
}
