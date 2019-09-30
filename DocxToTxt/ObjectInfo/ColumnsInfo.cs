using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class ColumnsInfo
    {
        public ColumnInfo[] Columns = new ColumnInfo[0];
        public bool EqualWidth { get; set; } = true;
        public bool Separator { get; set; } = false;

        public ColumnsInfo() { }
    }
}
