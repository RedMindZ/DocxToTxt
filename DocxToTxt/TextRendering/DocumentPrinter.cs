using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocxToTxt.NumberingUtils;
using DocxToTxt.StyleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class DocumentPrinter
    {
        private readonly WordprocessingDocument _document;
        private readonly List<NumberingInfo> _documentNumberingInfo;
        private readonly StyleTree _documentStyleTree;



        public DocumentPrinter(WordprocessingDocument document)
        {
            _document = document;
            _documentNumberingInfo = NumberingHelper.BuildNumberingInfo(_document);
            _documentStyleTree = StyleHelper.ResolveStyleTreeInheritance(StyleHelper.BuildStyleTree(_document));
        }

        public List<TextPage> PrintDocument()
        {
            foreach (OpenXmlElement child in _document.MainDocumentPart.Document.Body)
            {
                switch (child)
                {
                    case Paragraph p:
                        {
                            break;
                        }

                    case Table t:
                        {
                            break;
                        }
                }
            }

            return null;
        }
    }
}
