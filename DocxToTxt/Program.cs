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
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine($"Expected at least 2 arguments. Instead got {args.Length}:");

                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine($"Argument {i + 1}: {args[i]}");
                }

                Console.WriteLine("Usage: DocxToTxt <InputFile> <OutputFile> [options]");

                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"The specified input file \"{args[0]}\" does not exist.");

                return;
            }

            if (File.Exists(args[1]))
            {
                Console.WriteLine($"The file \"{args[1]}\" already exists. Press \"Y\" if you wish to override it:");

                if (Console.ReadKey().Key != ConsoleKey.Y)
                {
                    return;
                }
            }

            TableBorderCharacterSet tbcs = TableBorderCharacterSet.EmptySet;

            for (int i = 2; i < args.Length; i++)
            {
                if (args[i] == "-e")
                {
                    tbcs = new TableBorderCharacterSet();
                }
            }

            string inputFilePath = args[0];
            string outputFilePath = args[1];

            using (WordprocessingDocument document = WordprocessingDocument.Open(inputFilePath, false))
            {
                TextElementDocument textElementDocument = new TextElementDocument(document)
                {
                    BorderCharacterSet = tbcs
                };
                textElementDocument.BuildDocument();

                ITextElement renderElement = textElementDocument;

                renderElement.Measure(new Size(int.MaxValue, 120));
                TextPage compositePage = renderElement.ToTextPage(renderElement.DesiredSize, ' ');
                string renderedString = compositePage.ToString();

                File.WriteAllText(outputFilePath, renderedString);
                Console.WriteLine(renderedString);
            }

            Console.WriteLine("~Done~");
            Console.ReadKey();
        }
    }
}
