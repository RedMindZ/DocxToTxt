using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocxToTxt.NumberingUtils;
using DocxToTxt.StyleUtils;
using DocxToTxt.TextRendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt
{
    public static class Program
    {
        private const string TestFolderPath = @"D:\Programing Stuff\C#\DocxToTxt\DocxToTxt\Testing\Samples\";

        public static void Main(string[] args)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Open(Path.Combine(TestFolderPath, "Test Document.docx"), false))
            {
                var doc = document.MainDocumentPart.Document;

                TextPageBuilder p1b = new TextPageBuilder(StaticStrings.ShortString1.Length)
                {
                    LineWrap = true,
                    TextOrientation = TextOrientationValues.LeftToRightTopToBottom,
                    TextAlignment = TextRendering.TextAlignment.Middle
                };
                p1b.Text.Append(StaticStrings.ShortString1);
                TextPage p1 = p1b.Build(' ').Page;

                TextElementDocker p1Docker = new TextElementDocker(p1)
                {
                    HorizontalDock = HorizontalDock.Middle,
                    VerticalDock = VerticalDock.Middle
                };

                TextPageBuilder p2b = new TextPageBuilder(40)
                {
                    LineWrap = true,
                    TextOrientation = TextOrientationValues.LeftToRightTopToBottom,
                    TextAlignment = TextRendering.TextAlignment.Middle
                };
                p2b.Text.Append(StaticStrings.LongString2);
                TextPage p2 = p2b.Build(' ').Page;

                TextPageBuilder p3b = new TextPageBuilder(40)
                {
                    LineWrap = true,
                    TextOrientation = TextOrientationValues.LeftToRightTopToBottom,
                    TextAlignment = TextRendering.TextAlignment.Middle
                };
                p3b.Text.Append(StaticStrings.LongString1);
                TextPage p3 = p3b.Build(' ').Page;

                TextElementStack pageStack = new TextElementStack(StackOrientation.Vertical)
                {
                    Spacing = 1
                };
                pageStack.Children.Add(p1Docker);
                pageStack.Children.Add(p2);
                pageStack.Children.Add(p3);

                TextElementPadding padding = new TextElementPadding(pageStack);
                padding.Padding.Top = 2;
                padding.Padding.Left = 4;
                padding.Padding.Bottom = 2;
                padding.Padding.Right = 4;

                //-------------------------------------------------------------

                TextElementTreeNode treeRoot = new TextElementTreeNode()
                {
                    ChildIndent = 0,
                    PrefixContentSpacing = 0,
                    HeaderChildrenSpacing = 0
                };

                TextElementTreeNode c01 = new TextElementTreeNode()
                {
                    Prefix = new TextPage(new char[,] { { '[', '1', ']' } }),
                    Content = p1
                };

                TextElementTreeNode c11 = new TextElementTreeNode()
                {
                    Prefix = new TextPage(new char[,] { { 'A', ')' } }),
                    Content = p2
                };

                TextElementTreeNode c12 = new TextElementTreeNode()
                {
                    Prefix = new TextPage(new char[,] { { 'B', ')' } }),
                    Content = p3
                };

                TextElementTreeNode c02 = new TextElementTreeNode()
                {
                    Prefix = new TextPage(new char[,] { { '[', '2', ']' } }),
                    Content = p1
                };

                TextElementTreeNode c13 = new TextElementTreeNode()
                {
                    Prefix = new TextPage(new char[,] { { 'A', ')' } }),
                    Content = p2
                };

                c01.AppendChild(c11);
                c01.AppendChild(c12);

                c02.AppendChild(c13);

                treeRoot.AppendChild(c01);
                treeRoot.AppendChild(c02);

                //-------------------------------------------------------------

                TextElementParagraph sp1 = new TextElementParagraph(StaticStrings.ShortString1)
                {
                    LineWrap = true,
                };

                TextElementParagraph lp1 = new TextElementParagraph(StaticStrings.LongString1)
                {
                    LineWrap = true,
                    TextAlignment = TextRendering.TextAlignment.Middle
                };

                TextElementParagraph lp2 = new TextElementParagraph(StaticStrings.LongString2)
                {
                    LineWrap = true,
                    TextOrientation = TextOrientationValues.RightToLeftTopToBottom,
                    TextAlignment = TextRendering.TextAlignment.Beginning
                };

                TextElementParagraph lp3 = new TextElementParagraph(StaticStrings.LongString3)
                {
                    LineWrap = true,
                };

                TextElementTable table = new TextElementTable(2, 2);
                table.TryMergeCells(new Rectangle(0, 1, 2, 1), out bool merged);
                table.SetColumnWidth(40);
                table.SetRowHeight(10);

                table.SetCellChild(sp1, new Index2D(0, 0));
                table.SetCellChild(lp2, new Index2D(1, 0));
                table.SetCellChild(lp1, new Index2D(0, 1));

                table.SetCellPadding(new Padding(1, 2, 1, 2), new Index2D(0, 0));
                table.SetCellPadding(new Padding(1, 2, 1, 2), new Index2D(1, 0));
                table.SetCellPadding(new Padding(1, 2, 1, 2), new Index2D(0, 1));

                //=============================================================

                ITextElement renderElement = treeRoot;

                renderElement.Measure(Size.MaxSize);
                TextPage compositePage = renderElement.ToTextPage(renderElement.DesiredSize, ' ');

                Console.WriteLine(compositePage);

                //File.WriteAllText("test.txt", sourcePage.ToString());

                //OpenXmlUtils.PrintTree(doc);
            }

            Console.WriteLine("~Done~");
            Console.ReadKey();
        }
    }
}
