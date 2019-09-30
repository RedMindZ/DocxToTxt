using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementTableRow
    {
        public int Height { get; set; } = 0;



        public TextElementTableRow() { }

        public TextElementTableRow(int height)
        {
            Height = height;
        }
    }
}
