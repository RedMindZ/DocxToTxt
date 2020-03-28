using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public delegate string LevelToString(int levelNumber, int levelIndex, TextOrientationValues orientation);

    public class LevelConverter
    {
        public LevelToString ToIndexString { get; set; } = LevelToDecimalString;
        public LevelToString ToDelimiterString { get; set; } = LevelToDotDelimiter;

        public static string LevelToDecimalString(int levelNumber, int levelIndex, TextOrientationValues orientation)
        {
            string s = levelNumber.ToString();

            if (OrientedTextPageView.IsOrientationVertical(orientation))
            {
                if (OrientedTextPageView.IsOrientationVerticallyReversed(orientation))
                {
                    s = new string(s.Reverse().ToArray());
                }
            }
            else
            {
                if (OrientedTextPageView.IsOrientationHorizontallyReversed(orientation))
                {
                    s = new string(s.Reverse().ToArray());
                }
            }

            return s;
        }

        public static string LevelToDotDelimiter(int numberingIndex, int levelIndex, TextOrientationValues orientation)
        {
            return ".";
        }
    }
}
