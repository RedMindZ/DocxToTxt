using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class Index2D
    {
        public int Row { get; set; } = 0;
        public int Column { get; set; } = 0;



        public Index2D() { }

        public Index2D(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Index2D(Index2D other)
        {
            Row = other.Row;
            Column = other.Column;
        }

        public static implicit operator Index2D((int row, int column) index)
        {
            return new Index2D(index.row, index.column);
        }
    }
}
