using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextPage : TextPageView, ITextElement
    {
        public char[,] CharBuffer { get; }

        public override int LineCount => CharBuffer.GetLength(0);
        public override int LineLength => CharBuffer.GetLength(1);

        public int Height => LineCount;
        public int Width => LineLength;

        public Size DesiredSize { get; protected set; }

        public override char this[int rowIndex, int columnIndex]
        {
            get
            {
                return CharBuffer[rowIndex, columnIndex];
            }

            set
            {
                CharBuffer[rowIndex, columnIndex] = value;
            }
        }



        public TextPage(int lineCount, int lineLength) : this(lineCount, lineLength, '\0') { }
        public TextPage(int lineCount, int lineLength, char fill)
        {
            CharBuffer = new char[lineCount, lineLength];

            for (int i = 0; i < LineCount; i++)
            {
                for (int j = 0; j < LineLength; j++)
                {
                    CharBuffer[i, j] = fill;
                }
            }
        }

        public TextPage(char[,] buffer)
        {
            CharBuffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        }



        public void Measure(Size maxSize)
        {
            DesiredSize = Size.MinPositiveSize(new Size(LineCount, LineLength), maxSize);
        }

        public TextPage ToTextPage(Size maxSize, char fill)
        {
            Size pageSize = Size.MinPositiveSize(DesiredSize, maxSize);
            TextPage page = new TextPage(pageSize.Height, pageSize.Width, fill);
            Blit(this, page, 0, 0);

            return page;
        }



        public static void Blit(TextPage sourcePage, TextPage destinationPage, int destinationX, int destinationY)
        {
            Blit(sourcePage, 0, 0, sourcePage.LineLength, sourcePage.LineCount, destinationPage, destinationX, destinationY);
        }

        public static void Blit(TextPage sourcePage, Rectangle sourceRect, TextPage destinationPage, int destinationX, int destinationY)
        {
            Blit(sourcePage, sourceRect.X, sourceRect.Y, sourceRect.Width, sourceRect.Height, destinationPage, destinationX, destinationY);
        }

        public static void Blit(TextPage sourcePage, int sourceX, int sourceY, int sourceWidth, int sourceHeight, TextPage destinationPage, int destinationX, int destinationY)
        {
            if (sourceWidth < 0 || sourceHeight < 0)
            {
                throw new ArgumentException($"{nameof(sourceWidth)} and {nameof(sourceHeight)} must both be non-negative.");
            }

            int ProjectIndex(int row, int column, int rowLength) => row * rowLength + column;

            int sourceMinX = MathUtils.Clamp(sourceX, 0, sourcePage.LineLength);
            int sourceMinY = MathUtils.Clamp(sourceY, 0, sourcePage.LineCount);
            int sourceMaxX = MathUtils.Clamp(sourceX + sourceWidth, 0, sourcePage.LineLength);
            int sourceMaxY = MathUtils.Clamp(sourceY + sourceHeight, 0, sourcePage.LineCount);

            int sourceClampedWidth = sourceMaxX - sourceMinX;
            int sourceClampedHeight = sourceMaxY - sourceMinY;

            int sourceLeftMargin = sourceMinX - sourceX;
            int sourceTopMargin = sourceMinY - sourceY;
            int sourceRightMargin = sourceX + sourceWidth - sourceMaxX;
            int sourceBottomMargin = sourceY + sourceHeight - sourceMaxY;

            if (sourceLeftMargin < 0 || sourceTopMargin < 0 || sourceRightMargin < 0 || sourceBottomMargin < 0)
            {
                // In this case, the source rectangle does not intersect
                // the source page, and therefore there is nothing to blit.
                return;
            }

            int destinationMinX = MathUtils.Clamp(destinationX + sourceLeftMargin, 0, destinationPage.LineLength);
            int destinationMinY = MathUtils.Clamp(destinationY + sourceTopMargin, 0, destinationPage.LineCount);
            int destinationMaxX = MathUtils.Clamp(destinationX + sourceWidth - sourceRightMargin, 0, destinationPage.LineLength);
            int destinationMaxY = MathUtils.Clamp(destinationY + sourceHeight - sourceBottomMargin, 0, destinationPage.LineCount);

            int destinationClampedWidth = destinationMaxX - destinationMinX;
            int destinationClampedHeight = destinationMaxY - destinationMinY;

            int destinationLeftClip = destinationMinX - destinationX;
            int destinationTopClip = destinationMinY - destinationY;

            sourceMinX += destinationLeftClip - sourceLeftMargin;
            sourceMinY += destinationTopClip - sourceTopMargin;

            int numRowsToCopy = Math.Min(sourceClampedHeight, destinationClampedHeight);
            int rowWidth = Math.Min(sourceClampedWidth, destinationClampedWidth);

            for (int i = 0; i < numRowsToCopy; i++)
            {
                Array.Copy
                (
                    sourcePage.CharBuffer,
                    ProjectIndex(sourceMinY + i, sourceMinX, sourcePage.LineLength),
                    destinationPage.CharBuffer,
                    ProjectIndex(destinationMinY + i, destinationMinX, destinationPage.LineLength),
                    rowWidth
                );
            }
        }
    }
}
