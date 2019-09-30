using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class RunInfo
    {
        public bool Bold { get; set; } = false;
        public bool Italic { get; set; } = false;
        public bool Caps { get; set; } = false;
        public ColorInfo Color { get; set; } = new ColorInfo();
        public bool DoubleStrike { get; set; } = false;
        public bool Emboss { get; set; } = false;
        public bool Imprint { get; set; } = false;
        public bool Outline { get; set; } = false;
        // rStyle is not implemented, because the style properties will be written directly to the object;
        public bool Shadow { get; set; } = false;
        public bool SmallCaps { get; set; } = false;
        public bool Strike { get; set; } = false;
        public int Size { get; set; } = 0;
        public UnderlineInfo Underline { get; set; } = new UnderlineInfo();
        public bool Vanish { get; set; } = false;
        public VerticalPositionValues VerticalAlignment { get; set; } = VerticalPositionValues.Baseline;
        public RunBorderInfo Border { get; set; } = new RunBorderInfo();
        public RunFontsInfo Fonts { get; set; } = new RunFontsInfo();
        public ShadingInfo Shading { get; set; } = new ShadingInfo();
        public HighlightColorValues Highlight { get; set; } = HighlightColorValues.Black;
        public RunSpacingInfo Spacing { get; set; } = new RunSpacingInfo();

        public RunInfo() { }
    }
}
