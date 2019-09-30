using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public enum TextAlignment
    {
        Beginning,
        Middle,
        End
    }

    public class TextPageBuilder
    {
        public StringBuilder Text { get; } = new StringBuilder();

        public bool LineWrap { get; set; } = false;
        public TextOrientationValues TextOrientation { get; set; } = TextOrientationValues.LeftToRightTopToBottom;
        public TextAlignment TextAlignment { get; set; } = TextAlignment.Beginning;
        public int MaxLineLength { get; set; }



        public TextPageBuilder(int maxLineLength)
        {
            MaxLineLength = maxLineLength;
        }

        public TextPageBuilderResult Build(char fill = ' ')
        {
            string pageText = Text.ToString();
            List<string> pageLines = SplitTextLines(pageText, MaxLineLength, LineWrap);

            TextPage page = OrientedTextPageView.CreateOrientedTextPage(pageLines.Count, MaxLineLength, TextOrientation, fill);
            TextPageView pageView = new OrientedTextPageView(page, TextOrientation);

            for (int i = 0; i < pageLines.Count; i++)
            {
                int charIndexOffset = 0;

                if (TextAlignment == TextAlignment.Beginning)
                {
                    charIndexOffset = 0;
                }
                else if (TextAlignment == TextAlignment.Middle)
                {
                    charIndexOffset = (MaxLineLength - pageLines[i].Length) / 2;
                }
                else if (TextAlignment == TextAlignment.End)
                {
                    charIndexOffset = MaxLineLength - pageLines[i].Length;
                }

                pageView.WriteString(pageLines[i], i, charIndexOffset);
            }

            return new TextPageBuilderResult
            {
                Page = page,
                PageView = pageView,
                Lines = pageLines
            };
        }



        public static List<string> SplitTextLines(string str, int maxLineLength, bool lineWrap)
        {
            if (lineWrap)
            {
                return StringToWrappedLines(str, maxLineLength);
            }
            else
            {
                return StringToChunks(str, maxLineLength);
            }
        }

        private static List<string> StringToChunks(string str, int maxLineLength)
        {
            List<string> lines = new List<string>();

            int currentIndex = 0;

            int bufferIndex = 0;
            int bufferLength = 0;
            char[] lineBuffer = new char[maxLineLength];

            bool hasEmptyLine = false;

            void BreakLine()
            {
                lines.Add(new string(lineBuffer, 0, bufferLength));

                Array.Clear(lineBuffer, 0, lineBuffer.Length);

                bufferLength = 0;
                bufferIndex = 0;
            }

            while (currentIndex < str.Length)
            {
                char currentChar = str[currentIndex];

                hasEmptyLine = false;

                if (currentChar == '\n')
                {
                    BreakLine();
                    hasEmptyLine = true;
                }
                else if (currentChar == '\r')
                {
                    bufferIndex = 0;
                }
                else
                {
                    lineBuffer[bufferIndex] = currentChar;
                    bufferIndex++;

                    bufferLength = Math.Max(bufferLength, bufferIndex);
                }

                if (bufferIndex == lineBuffer.Length)
                {
                    BreakLine();
                }

                currentIndex++;
            }

            if (bufferLength > 0 || hasEmptyLine)
            {
                BreakLine();
            }

            return lines;
        }

        private static List<string> StringToWrappedLines(string str, int maxLineLength)
        {
            if (maxLineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLineLength), "The maximum line length must be positive.");
            }

            List<string> lines = new List<string>();

            int lastLWB = 0;
            int lastRWB = 0;
            int currentIndex = 0;

            bool updateBuffer = true;
            int bufferIndex = 0;
            int bufferLength = 0;
            char[] lineBuffer = new char[maxLineLength];

            bool hasEmptyLine = false;

            void BreakLine()
            {
                lines.Add(new string(lineBuffer, 0, bufferLength));

                Array.Clear(lineBuffer, 0, lineBuffer.Length);

                if (lastRWB < lastLWB)
                {
                    str.CopyTo(lastLWB + 1, lineBuffer, 0, currentIndex - lastLWB);
                    bufferLength = currentIndex - lastLWB;
                    bufferIndex = bufferLength;
                }
                else
                {
                    updateBuffer = false;
                    bufferLength = 0;
                    bufferIndex = 0;
                }
            }

            while (currentIndex < str.Length)
            {
                char currentChar = str[currentIndex];

                if (IsRightWordBoundary(str, currentIndex))
                {
                    lastRWB = currentIndex;
                }
                else if (IsLeftWordBoundary(str, currentIndex))
                {
                    lastLWB = currentIndex;
                    //updateBuffer = true;
                }

                if (currentChar == '\n')
                {
                    BreakLine();
                    hasEmptyLine = true;
                }
                else if (currentChar == '\r')
                {
                    bufferIndex = 0;
                    updateBuffer = false;
                }
                else
                {
                    if (updateBuffer)
                    {
                        lineBuffer[bufferIndex] = currentChar;
                        bufferIndex++;
                        hasEmptyLine = false;
                    }

                    if (currentIndex == lastRWB)
                    {
                        bufferLength = Math.Max(bufferLength, bufferIndex);
                    }

                }

                if (currentIndex == lastLWB)
                {
                    updateBuffer = true;
                }

                if (bufferIndex == lineBuffer.Length)
                {
                    bool wordTooLongForBuffer = (currentIndex - lastLWB == lineBuffer.Length) && (lastRWB != currentIndex);

                    if (wordTooLongForBuffer)
                    {
                        lastRWB = currentIndex;
                        lastLWB = currentIndex;

                        bufferLength = lineBuffer.Length;
                    }

                    BreakLine();

                    if (wordTooLongForBuffer)
                    {
                        updateBuffer = true;
                    }
                }

                currentIndex++;
            }

            if (bufferLength > 0 || hasEmptyLine)
            {
                BreakLine();
            }

            return lines;
        }

        private static bool IsLeftWordBoundary(string str, int currentIndex)
        {
            int nextIndex = currentIndex + 1;
            return nextIndex < str.Length && (currentIndex < 0 || char.IsWhiteSpace(str[currentIndex])) && (nextIndex >= 0 && !char.IsWhiteSpace(str[nextIndex]));
        }

        private static bool IsLeftWordBoundary(char[] str, int currentIndex)
        {
            int nextIndex = currentIndex + 1;
            return nextIndex < str.Length && (currentIndex < 0 || char.IsWhiteSpace(str[currentIndex])) && (nextIndex >= 0 && !char.IsWhiteSpace(str[nextIndex]));
        }

        private static bool IsRightWordBoundary(string str, int currentIndex)
        {
            int nextIndex = currentIndex + 1;
            return currentIndex < str.Length && (nextIndex >= str.Length || char.IsWhiteSpace(str[nextIndex])) && (currentIndex >= 0 && !char.IsWhiteSpace(str[currentIndex]));
        }

        private static bool IsRightWordBoundary(char[] str, int currentIndex)
        {
            int nextIndex = currentIndex + 1;
            return currentIndex < str.Length && (nextIndex >= str.Length || char.IsWhiteSpace(str[nextIndex])) && (currentIndex >= 0 && !char.IsWhiteSpace(str[currentIndex]));
        }
    }
}
