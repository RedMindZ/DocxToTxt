using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class SectionFooterInfo
    {
        public FooterInfo Footer { get; set; } = new FooterInfo();
        public HeaderFooterValues Type { get; set; } = HeaderFooterValues.Default;

        public SectionFooterInfo() { }
    }
}
