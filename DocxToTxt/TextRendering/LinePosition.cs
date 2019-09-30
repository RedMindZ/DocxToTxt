using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class LinePosition
    {
        public int LineIndex { get; set; }
        public int CharIndex { get; set; }



        public LinePosition() { }

        public LinePosition(int lineIndex, int charIndex)
        {
            LineIndex = lineIndex;
            CharIndex = charIndex;
        }

        public LinePosition(LinePosition other)
        {
            LineIndex = other.LineIndex;
            CharIndex = other.CharIndex;
        }

        public LinePosition Clone()
        {
            return new LinePosition(this);
        }



        public void MoveNext(int lineLength)
        {
            Move(1, lineLength);
        }

        public void Move(int charCount, int lineLength)
        {
            LinePosition nextPos = ComputeAdvancedLinePosition(this, charCount, lineLength);

            LineIndex = nextPos.LineIndex;
            CharIndex = nextPos.CharIndex;
        }



        public static LinePosition ComputeNextLinePosition(LinePosition linePos, int lineLength)
        {
            return ComputeNextLinePosition(linePos.LineIndex, linePos.CharIndex, lineLength);
        }

        public static LinePosition ComputeNextLinePosition(int lineIndex, int charIndex, int lineLength)
        {
            return ComputeAdvancedLinePosition(lineIndex, charIndex, 1, lineLength);
        }

        public static LinePosition ComputeAdvancedLinePosition(LinePosition linePos, int charCount, int lineLength)
        {
            return ComputeAdvancedLinePosition(linePos.LineIndex, linePos.CharIndex, charCount, lineLength);
        }

        public static LinePosition ComputeAdvancedLinePosition(int lineIndex, int charIndex, int charCount, int lineLength)
        {
            return new LinePosition
            {
                CharIndex = (charIndex + charCount) % (lineLength),
                LineIndex = (charIndex + charCount) / (lineLength) + lineIndex
            };
        }
    }
}
