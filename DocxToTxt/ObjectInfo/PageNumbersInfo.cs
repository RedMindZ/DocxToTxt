using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class PageNumbersInfo
    {
        public ChapterSeparatorValues ChapterSeparator { get; set; } = ChapterSeparatorValues.Hyphen;
        public int ChapterStyle { get; set; } = 0;
        public NumberFormatValues Format { get; set; } = NumberFormatValues.Decimal;
        public int Start { get; set; } = 1;

        public PageNumbersInfo() { }
    }
}
