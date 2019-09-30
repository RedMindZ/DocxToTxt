using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt
{
    public static class OpenXmlUtils
    {
        public static void IterateTree(OpenXmlElement root, Action<OpenXmlElement, int> elementwiseAction, int depth = 0)
        {
            elementwiseAction(root, depth);

            foreach (var child in root)
            {
                IterateTree(child, elementwiseAction, depth + 1);
            }
        }

        public static List<OpenXmlElement> FilterTree(OpenXmlElement root, Func<OpenXmlElement, int, bool> filterFunc)
        {
            List<OpenXmlElement> elements = new List<OpenXmlElement>();

            void filter(OpenXmlElement element, int depth)
            {
                if (filterFunc(element, depth))
                {
                    elements.Add(element);
                }
            }

            IterateTree(root, filter);

            return elements;
        }

        public static void PrintTree(OpenXmlElement root)
        {
            void printFunc(OpenXmlElement element, int depth)
            {
                Console.Write(new string(' ', depth * 4));
                Console.Write(element.GetType().Name);

                if (element is Text t)
                {
                    Console.Write(": [" + t.Text + "]");
                }

                Console.WriteLine();
            }

            IterateTree(root, printFunc);
        }
    }
}
