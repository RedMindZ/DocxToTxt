using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public enum TextOrientationValues
    {
        // Horizontal/Vertical, Left/Right, Top/Bottom
        LeftToRightTopToBottom = 0b000,
        LeftToRightBottomToTop = 0b001,
        RightToLeftTopToBottom = 0b010,
        RightToLeftBottomToTop = 0b011,
        TopToBottomLeftToRight = 0b100,
        BottomToTopLeftToRight = 0b101,
        TopToBottomRightToLeft = 0b110,
        BottomToTopRightToLeft = 0b111,
    }

    public class OrientedTextPageView : TextPageView
    {
        public TextOrientationValues TextOrientation { get; set; } = TextOrientationValues.LeftToRightTopToBottom;

        public TextPageView Page { get; }

        public override int LineCount => IsHorizontal ? Page.LineCount : Page.LineLength;
        public override int LineLength => IsHorizontal ? Page.LineLength : Page.LineCount;

        public bool IsVertical => IsOrientationVertical(TextOrientation);
        public bool IsHorizontal => !IsVertical;

        public OrientedTextPageView(TextPageView page)
        {
            Page = page;
        }

        public OrientedTextPageView(TextPageView page, TextOrientationValues orientation)
        {
            Page = page;
            TextOrientation = orientation;
        }

        public static TextPage CreateOrientedTextPage(int lineCount, int lineLength, TextOrientationValues orientation, char fill = ' ')
        {
            if (IsOrientationVertical(orientation))
            {
                return new TextPage(lineLength, lineCount, fill);
            }
            else
            {
                return new TextPage(lineCount, lineLength, fill);
            }
        }

        public override char this[int lineIndex, int charIndex]
        {
            get
            {
                Point translatedLinePos = TranslateLinePosition(lineIndex, charIndex, Page, TextOrientation);
                return Page[translatedLinePos.Y, translatedLinePos.X];
            }

            set
            {
                Point translatedLinePos = TranslateLinePosition(lineIndex, charIndex, Page, TextOrientation);
                Page[translatedLinePos.Y, translatedLinePos.X] = value;
            }
        }

        // Note that this function is its own inverse
        public static Point TranslateLinePosition(int lineIndex, int charIndex, TextPageView page, TextOrientationValues orientation) => TranslateLinePosition(lineIndex, charIndex, page.LineLength, page.LineCount, orientation);
        public static Point TranslateLinePosition(int lineIndex, int charIndex, int pageLineLength, int pageLineCount, TextOrientationValues orientation)
        {
            int row = lineIndex;
            int col = charIndex;

            if (IsOrientationVertical(orientation))
            {
                row = charIndex;
                col = lineIndex;
            }

            if (IsOrientationHorizontallyReversed(orientation))
            {
                col = pageLineLength - col - 1;
            }

            if (IsOrientationVerticallyReversed(orientation))
            {
                row = pageLineCount - row - 1;
            }

            return new Point { X = col, Y = row };
        }

        public static bool IsOrientationVertical(TextOrientationValues orientation)
        {
            return ((int)orientation & 0b100) == 0b100;
        }

        public static bool IsOrientationHorizontallyReversed(TextOrientationValues orientation)
        {
            return ((int)orientation & 0b010) == 0b010;
        }

        public static bool IsOrientationVerticallyReversed(TextOrientationValues orientation)
        {
            return ((int)orientation & 0b001) == 0b001;
        }
    }
}
