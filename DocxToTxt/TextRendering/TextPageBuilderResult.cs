using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextPageBuilderResult
    {
        public TextPage Page { get; set; }
        public TextPageView PageView { get; set; }
        public List<string> Lines { get; set; }
    }
}
