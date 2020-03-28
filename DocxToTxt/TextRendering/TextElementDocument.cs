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
        private readonly StyleTree _documentStyleTree;
        private readonly List<NumberingInfo> _documentNumberingInfo;

        private Dictionary<int, NumberingCounter> _numberingCounters;

        private TextElementStack _documentStack;

        public TableBorderCharacterSet BorderCharacterSet { get; set; } = new TableBorderCharacterSet();



        public TextElementDocument(WordprocessingDocument document)
        {
            _document = document;
            _documentStyleTree = StyleHelper.ResolveStyleTreeInheritance(StyleHelper.BuildStyleTree(_document));
            _documentNumberingInfo = NumberingHelper.BuildNumberingInfo(_document);
        }

        public void BuildDocument()
        {
            _documentStack = new TextElementStack(StackOrientation.Vertical)
            {
                Spacing = 0
            };

            PopulateStack();
        }

        private void PopulateStack()
        {
            _numberingCounters = new Dictionary<int, NumberingCounter>();

            foreach (NumberingInfo numInfo in _documentNumberingInfo)
            {
                NumberingCounter counter = new NumberingCounter(numInfo.Levels.Count);

                foreach (Level level in numInfo.Levels)
                {
                    counter.SetCounterInitialNumber(level.LevelIndex, level.StartNumberingValue?.Val ?? 0);
                    counter.SetCounterResetIndex(level.LevelIndex, level.LevelRestart?.Val ?? level.LevelIndex);
                }

                _numberingCounters.Add(numInfo.NumberingInstance.NumberID, counter);
            }

            foreach (OpenXmlElement docElement in _document.MainDocumentPart.Document.Body)
            {
                _documentStack.Children.Add(TranslateElement(docElement));
            }
        }

        private ITextElement TranslateElement(OpenXmlElement element)
        {
            switch (element)
            {
                case Paragraph p:
                    return TranslateParagraph(p);

                case Table t:
                    return TranslateTable(t);

                default:
                    return new TextPage(0, 0);
            }
        }

        private ITextElement TranslateParagraph(Paragraph p)
        {
            ITextElement topLevelElement;

            TextOrientationValues paragraphOrientation = (p.ParagraphProperties != null && p.ParagraphProperties.BiDi != null) ? TextOrientationValues.RightToLeftTopToBottom : TextOrientationValues.LeftToRightTopToBottom;
            TextAlignment paragraphAlignment = OrientedTextPageView.IsOrientationHorizontallyReversed(paragraphOrientation) ? TextAlignment.End : TextAlignment.Beginning;

            TextElementParagraph textElementParagraph = new TextElementParagraph()
            {
                LineWrap = true,
                //TextAlignment = paragraphAlignment
            };

            foreach (Run run in p.ChildElements.OfType<Run>())
            {
                foreach (Text text in run.ChildElements.OfType<Text>())
                {
                    textElementParagraph.Text += text.Text;
                }
            }

            topLevelElement = textElementParagraph;

            if (p.ParagraphProperties != null && p.ParagraphProperties.NumberingProperties != null)
            {
                NumberingProperties numberingProps = p.ParagraphProperties.NumberingProperties;
                NumberingInfo numberingInfo = _documentNumberingInfo.Find(x => x.NumberingInstance.NumberID.Value == numberingProps.NumberingId.Val.Value);
                NumberingCounter counter = _numberingCounters[numberingInfo.NumberingInstance.NumberID];

                int levelIndex = numberingProps.NumberingLevelReference.Val.Value;
                int levelNumber = counter.GetNextLevelNumber(levelIndex);

                TextElementNumberingLevel textElementLevel = new TextElementNumberingLevel(levelNumber, levelIndex, topLevelElement)
                {
                    //Orientation = paragraphOrientation
                };

                topLevelElement = textElementLevel;

                counter.UpdateCouters(levelIndex);
            }

            TextElementDocker textElementDocker = new TextElementDocker(topLevelElement);

            //if (OrientedTextPageView.IsOrientationHorizontallyReversed(paragraphOrientation))
            //{
            //    textElementDocker.HorizontalDock = HorizontalDock.Right;
            //}
            //else
            //{
            //    textElementDocker.HorizontalDock = HorizontalDock.Left;
            //}

            topLevelElement = textElementDocker;

            return topLevelElement;
        }

        private ITextElement TranslateTable(Table t)
        {
            int[] columnWidths = t.OfType<TableGrid>().First().OfType<GridColumn>().Select(x => DocxUnitConverter.TwipsToCharacters(int.Parse(x.Width))).ToArray();
            TableRow[] tableRows = t.OfType<TableRow>().ToArray();

            TextElementTable table = new TextElementTable(tableRows.Length, columnWidths.Length)
            {
                BorderCharacterSet = BorderCharacterSet
            };

            for (int i = 0; i < columnWidths.Length; i++)
            {
                table.SetColumnWidth(columnWidths[i], i);
            }

            for (int i = 0; i < tableRows.Length; i++)
            {
                TableRow currentRow = tableRows[i];

                TableCell[] rowCells = currentRow.OfType<TableCell>().ToArray();
                int maxCellHeight = 0;

                for (int j = 0; j < rowCells.Length; j++)
                {
                    TextElementStack cellStack = new TextElementStack(StackOrientation.Vertical);

                    foreach (OpenXmlElement child in rowCells[j])
                    {
                        cellStack.Children.Add(TranslateElement(child));
                    }

                    cellStack.Measure(new Size(int.MaxValue, columnWidths[j]));
                    maxCellHeight = Math.Max(maxCellHeight, cellStack.DesiredSize.Height);

                    table.SetCellChild(cellStack, new Index2D(i, j));
                }

                table.SetRowHeight(maxCellHeight, i);
            }

            return table;
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
