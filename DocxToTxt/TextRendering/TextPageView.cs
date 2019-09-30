using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public abstract class TextPageView
    {
        public abstract int LineCount { get; }
        public abstract int LineLength { get; }

        public abstract char this[int lineIndex, int charIndex] { get; set; }

        public void WriteString(string str, int startLineIndex, int startCharIndex)
        {
            if (startLineIndex < 0 || startLineIndex >= LineCount)
            {
                throw new ArgumentOutOfRangeException(nameof(startLineIndex), $"The parameter {nameof(startLineIndex)} must be non-negative and less then {nameof(LineCount)}.");
            }

            if (startCharIndex < 0 || startCharIndex >= LineLength)
            {
                throw new ArgumentOutOfRangeException(nameof(startCharIndex), $"The parameter {nameof(startCharIndex)} must be non-negative and less then {nameof(LineLength)}.");
            }

            LinePosition currentLinePos = new LinePosition(startLineIndex, startCharIndex);

            for (int i = 0; i < str.Length; i++)
            {
                this[currentLinePos.LineIndex, currentLinePos.CharIndex] = str[i];

                currentLinePos.MoveNext(LineLength);

                if (currentLinePos.LineIndex >= LineCount)
                {
                    break;
                }
            }
        }

        public override string ToString() => ToString("\n");
        public string ToString(string seperator)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < LineCount; i++)
            {
                for (int j = 0; j < LineLength; j++)
                {
                    sb.Append(this[i, j]);
                }

                if (i + 1 < LineCount)
                {
                    sb.Append(seperator);
                }
            }

            return sb.ToString();
        }

        public static void Copy
        (
            TextPageView sourcePage,
            int sourceLineIndex,
            int sourceCharIndex,
            int sourceLineCount,
            int sourceLineLength,
            TextPageView destinationPage,
            int destinationLineIndex,
            int destinationCharIndex
        )
        {
            if (sourceLineLength < 0 || sourceLineCount < 0)
            {
                throw new ArgumentException($"{nameof(sourceLineLength)} and {nameof(sourceLineCount)} must both be non-negative.");
            }

            int sourceMinCharIndex = MathUtils.Clamp(sourceCharIndex, 0, sourcePage.LineLength);
            int sourceMinLineIndex = MathUtils.Clamp(sourceLineIndex, 0, sourcePage.LineCount);
            int sourceMaxCharIndex = MathUtils.Clamp(sourceCharIndex + sourceLineLength, 0, sourcePage.LineLength);
            int sourceMaxLineIndex = MathUtils.Clamp(sourceLineIndex + sourceLineCount, 0, sourcePage.LineCount);

            int sourceClampedLineLength = sourceMaxCharIndex - sourceMinCharIndex;
            int sourceClampedLineCount = sourceMaxLineIndex - sourceMinLineIndex;

            int sourceLeftMargin = sourceMinCharIndex - sourceCharIndex;
            int sourceTopMargin = sourceMinLineIndex - sourceLineIndex;
            int sourceRightMargin = sourceCharIndex + sourceLineLength - sourceMaxCharIndex;
            int sourceBottomMargin = sourceLineIndex + sourceLineCount - sourceMaxLineIndex;

            if (sourceLeftMargin < 0 || sourceTopMargin < 0 || sourceRightMargin < 0 || sourceBottomMargin < 0)
            {
                // In this case, the source rectangle does not intersect
                // the source page, and therefore there is nothing to blit.
                return;
            }

            int destinationMinCharIndex = MathUtils.Clamp(destinationCharIndex + sourceLeftMargin, 0, destinationPage.LineLength);
            int destinationMinLineIndex = MathUtils.Clamp(destinationLineIndex + sourceTopMargin, 0, destinationPage.LineCount);
            int destinationMaxCharIndex = MathUtils.Clamp(destinationCharIndex + sourceLineLength - sourceRightMargin, 0, destinationPage.LineLength);
            int destinationMaxLineIndex = MathUtils.Clamp(destinationLineIndex + sourceLineCount - sourceBottomMargin, 0, destinationPage.LineCount);

            int destinationClampedLineLength = destinationMaxCharIndex - destinationMinCharIndex;
            int destinationClampedLineCount = destinationMaxLineIndex - destinationMinLineIndex;

            int destinationLeftClip = destinationMinCharIndex - destinationCharIndex;
            int destinationTopClip = destinationMinLineIndex - destinationLineIndex;

            sourceMinCharIndex += destinationLeftClip - sourceLeftMargin;
            sourceMinLineIndex += destinationTopClip - sourceTopMargin;

            int numLinesToCopy = Math.Min(sourceClampedLineCount, destinationClampedLineCount);
            int lineLength = Math.Min(sourceClampedLineLength, destinationClampedLineLength);

            for (int i = 0; i < numLinesToCopy; i++)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    destinationPage[destinationMinLineIndex + i, destinationMinCharIndex + j] = sourcePage[sourceMinLineIndex + i, sourceMinCharIndex + j];
                }
            }
        }
    }
}
