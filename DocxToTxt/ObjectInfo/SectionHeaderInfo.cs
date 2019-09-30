using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class SectionHeaderInfo
    {
        public HeaderInfo Header { get; set; } = new HeaderInfo();
        public HeaderFooterValues Type { get; set; } = HeaderFooterValues.Default;

        public SectionHeaderInfo() { }
    }
}
