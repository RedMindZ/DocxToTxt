using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public enum PageFlowValues
    {
        TopToBottom,
        BottomToTop,
        LeftToRight,
        RightToLeft
    }

    public class TextPagePrinterV2
    {
        public PageFlowValues PageFlow { get; set; }
    }
}
