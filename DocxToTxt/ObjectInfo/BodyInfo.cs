using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class BodyInfo
    {
        public ParagraphInfo[] Paragraphs { get; set; } = new ParagraphInfo[0];
        public TableInfo[] Tables { get; set; } = new TableInfo[0];
    }
}
