using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class PageBorderInfo
    {
        public PageBorderDisplayValues Display { get; set; } = PageBorderDisplayValues.AllPages;
        public PageBorderOffsetValues OffsetFrom { get; set; } = PageBorderOffsetValues.Page;
        public PageBorderZOrderValues ZOrder { get; set; } = PageBorderZOrderValues.Back;
        public BorderSideInfo Top { get; set; } = new BorderSideInfo();
        public BorderSideInfo Bottom { get; set; } = new BorderSideInfo();
        public BorderSideInfo Left { get; set; } = new BorderSideInfo();
        public BorderSideInfo Right { get; set; } = new BorderSideInfo();

        public PageBorderInfo() { }
    }
}
