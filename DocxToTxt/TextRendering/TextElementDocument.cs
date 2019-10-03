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
    public class TextElementDocument : ITextElement
    {
        public Size DesiredSize => _documentStack.DesiredSize;

        private readonly WordprocessingDocument _document;
        private readonly List<NumberingInfo> _documentNumberingInfo;
        private readonly StyleTree _documentStyleTree;

        private readonly TextElementStack _documentStack;



        public TextElementDocument(WordprocessingDocument document)
        {
            _document = document;
            _documentNumberingInfo = NumberingHelper.BuildNumberingInfo(_document);
            _documentStyleTree = StyleHelper.ResolveStyleTreeInheritance(StyleHelper.BuildStyleTree(_document));

            _documentStack = new TextElementStack(StackOrientation.Vertical);
            PopulateStack();
        }

        private void PopulateStack()
        {
            foreach (OpenXmlElement docElement in _document.MainDocumentPart.Document.Body)
            {
                switch (docElement)
                {
                    case Paragraph p:
                        break;

                    case Table t:
                        break;
                }
            }
        }



        public void Measure(Size maxSize)
        {
            _documentStack.Measure(maxSize);
        }

        public TextPage ToTextPage(Size maxSize, char fill)
        {
            return _documentStack.ToTextPage(maxSize, fill);
        }
    }
}
