using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.StyleUtils
{
    public class StyleNode
    {
        public Style Style { get; set; }

        public StyleNode Parent { get; set; }
        public List<StyleNode> Children { get; set; } = new List<StyleNode>();



        public StyleNode(Style style)
        {
            Style = style;
        }
    }
}
