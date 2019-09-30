using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementTableColumn
    {
        public int Width { get; set; } = 0;



        public TextElementTableColumn() { }

        public TextElementTableColumn(int width)
        {
            Width = width;
        }
    }
}
