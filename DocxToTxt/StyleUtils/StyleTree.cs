using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.StyleUtils
{
    public class StyleTree
    {
        public StyleNode Root { get; set; }



        public StyleTree Clone()
        {
            return new StyleTree { Root = CloneHelper(Root) };
        }

        private StyleNode CloneHelper(StyleNode parent)
        {
            StyleNode parentClone = new StyleNode((Style)parent.Style.Clone());

            foreach (var child in parent.Children)
            {
                StyleNode childClone = CloneHelper(child);
                childClone.Parent = parentClone;
                parentClone.Children.Add(childClone);
            }

            return parentClone;
        }

        public List<StyleNode> Flatten()
        {
            return Filter((x, d) => true);
        }

        public List<StyleNode> Filter(Func<StyleNode, int, bool> filterFunc)
        {
            List<StyleNode> elements = new List<StyleNode>();

            void filter(StyleNode element, int depth)
            {
                if (filterFunc(element, depth))
                {
                    elements.Add(element);
                }
            }

            Iterate(filter);

            return elements;
        }

        public void Iterate(Action<StyleNode, int> elementwiseAction)
        {
            Iterate(Root, elementwiseAction);
        }

        private static void Iterate(StyleNode root, Action<StyleNode, int> elementwiseAction, int depth = 0)
        {
            elementwiseAction(root, depth);

            foreach (var child in root.Children)
            {
                Iterate(child, elementwiseAction, depth + 1);
            }
        }
    }
}
