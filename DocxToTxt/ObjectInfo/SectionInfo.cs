using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class SectionInfo
    {
        public ColumnsInfo Columns { get; set; } = new ColumnsInfo();
        public SectionFooterInfo[] SectionFooter { get; set; } = new SectionFooterInfo[0];
        public bool FormProtection { get; set; } = false;
        public SectionHeaderInfo[] SectionHeader { get; set; } = new SectionHeaderInfo[0];
        public LineNumbersInfo LineNumbers { get; set; } = new LineNumbersInfo();
        public PageBorderInfo PageBorder { get; set; } = new PageBorderInfo();
        public PageMarginInfo PageMargin { get; set; } = new PageMarginInfo();
        public PageNumbersInfo PageNumbers { get; set; } = new PageNumbersInfo();
    }
}
