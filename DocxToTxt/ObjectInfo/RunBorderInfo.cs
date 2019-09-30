using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class RunBorderInfo : BorderSideInfo
    {
        public bool Frame { get; set; } = false;

        public RunBorderInfo() { }
    }
}
