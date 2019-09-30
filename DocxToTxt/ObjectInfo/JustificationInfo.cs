using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.ObjectInfo
{
    public class JustificationInfo
    {
        public JustificationValues Value { get; set; } = JustificationValues.Start;

        public JustificationInfo() { }
    }
}
