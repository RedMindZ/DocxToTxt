using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementTreeNode : ITextElement
    {
        public Size DesiredSize { get; protected set; }

        private ITextElement _prefix = new TextPage(0, 0);
        public ITextElement Prefix
        {
            get
            {
                return _prefix;
            }

            set
            {
                if (_prefix == null)
                {
                    _prefix = new TextPage(0, 0);
                }
                else
                {
                    _prefix = value;
                }
            }
        }

        private ITextElement _content = new TextPage(0, 0);
        public ITextElement Content
        {
            get
            {
                return _content;
            }

            set
            {
                if (_content == null)
                {
                    _content = new TextPage(0, 0);
                }
                else
                {
                    _content = value;
                }
            }
        }

        public int ChildIndent { get; set; } = 4;
        public int ChildSpacing { get; set; } = 1;
        public int HeaderChildrenSpacing { get; set; } = 1;
        public int PrefixContentSpacing { get; set; } = 1;

        public TextElementTreeNode ParentNode { get; set; } = null;
        public TextElementTreeNode PreviousNode { get; set; } = null;
        public TextElementTreeNode NextNode { get; set; } = null;
        public TextElementTreeNode FirstChild { get; set; } = null;



        public TextElementTreeNode() { }



        public void AppendChild(TextElementTreeNode child)
        {
            child.ParentNode = this;

            if (FirstChild == null)
            {
                FirstChild = child;
            }
            else
            {
                TextElementTreeNode currentNode = FirstChild;

                while (currentNode.NextNode != null)
                {
                    currentNode = currentNode.NextNode;
                }

                currentNode.NextNode = child;
                child.PreviousNode = currentNode;
            }
        }



        public void Measure(Size maxSize)
        {
            Prefix.Measure(maxSize);
            Content.Measure(new Size(maxSize.Height, maxSize.Width - Prefix.DesiredSize.Width - PrefixContentSpacing));
            Size prefixContentSize = new Size(Math.Max(Prefix.DesiredSize.Height, Content.DesiredSize.Height), Prefix.DesiredSize.Width + PrefixContentSpacing + Content.DesiredSize.Width);

            Size childrenSize = new Size(0, 0);
            Size sizeLeft = new Size(maxSize.Height - prefixContentSize.Height - HeaderChildrenSpacing, maxSize.Width - ChildIndent);
            TextElementTreeNode currentChild = FirstChild;

            while (currentChild != null)
            {
                currentChild.Measure(sizeLeft);
                childrenSize.Height += currentChild.DesiredSize.Height + (currentChild.NextNode != null ? ChildSpacing : 0);
                childrenSize.Width = Math.Max(childrenSize.Width, currentChild.DesiredSize.Width);

                sizeLeft.Height = sizeLeft.Height - currentChild.DesiredSize.Height - ChildSpacing;
                currentChild = currentChild.NextNode;
            }

            DesiredSize = new Size(prefixContentSize.Height + (FirstChild != null ? HeaderChildrenSpacing : 0) + childrenSize.Height, Math.Max(prefixContentSize.Width, ChildIndent + childrenSize.Width));
        }

        public TextPage ToTextPage(Size maxSize, char fill)
        {
            TextPage prefixPage = Prefix.ToTextPage(maxSize, fill);
            TextPage contentPage = Content.ToTextPage(new Size(maxSize.Height, maxSize.Width - prefixPage.LineLength - PrefixContentSpacing), fill);
            Size prefixContentSize = new Size(Math.Max(prefixPage.LineCount, contentPage.LineCount), prefixPage.LineLength + PrefixContentSpacing + contentPage.LineLength);

            Size childrenSize = new Size(0, 0);
            Size sizeLeft = new Size(maxSize.Height - prefixContentSize.Height - HeaderChildrenSpacing, maxSize.Width - ChildIndent);
            TextElementTreeNode currentChild = FirstChild;
            List<TextPage> childPages = new List<TextPage>();

            while (currentChild != null)
            {
                TextPage currentPage = currentChild.ToTextPage(sizeLeft, fill);
                childPages.Add(currentPage);

                childrenSize.Height += currentPage.LineCount + (currentChild.NextNode != null ? ChildSpacing : 0);
                childrenSize.Width = Math.Max(childrenSize.Width, currentPage.LineLength);

                sizeLeft.Height = sizeLeft.Height - currentPage.LineCount - ChildSpacing;
                currentChild = currentChild.NextNode;
            }

            Size finalPageSize = new Size(prefixContentSize.Height + childrenSize.Height + (FirstChild != null ? HeaderChildrenSpacing : 0), Math.Max(prefixContentSize.Width, ChildIndent + childrenSize.Width));

            TextPage page = new TextPage(finalPageSize.Height, finalPageSize.Width, fill);
            LinePosition pagePosition = new LinePosition(0, 0);

            TextPage.Blit(prefixPage, page, pagePosition.CharIndex, pagePosition.LineIndex);
            pagePosition.CharIndex += prefixPage.LineLength + PrefixContentSpacing;

            TextPage.Blit(contentPage, page, pagePosition.CharIndex, pagePosition.LineIndex);
            pagePosition.LineIndex = prefixContentSize.Height + HeaderChildrenSpacing;
            pagePosition.CharIndex = ChildIndent;

            for (int i = 0; i < childPages.Count; i++)
            {
                TextPage.Blit(childPages[i], page, pagePosition.CharIndex, pagePosition.LineIndex);

                pagePosition.LineIndex += childPages[i].LineCount + ChildSpacing;
            }

            return page;
        }
    }
}
