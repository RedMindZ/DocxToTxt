using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementLevelIndex : ITextElement
    {
        public Size DesiredSize { get; protected set; }

        public int LevelIndex { get; set; } = 0;
        public TextOrientationValues Orientation { get; set; } = TextOrientationValues.LeftToRightTopToBottom;
        public LevelConverter LevelConverter { get; set; } = new LevelConverter();



        public TextElementLevelIndex() { }

        public TextElementLevelIndex(int levelIndex, TextOrientationValues orientation)
        {
            LevelIndex = levelIndex;
            Orientation = orientation;
        }



        private TextPage BuildTextPage(Size maxSize, char fill)
        {
            return new TextPage(0, 0);
        }

        public void Measure(Size maxSize)
        {
            throw new NotImplementedException();
        }

        public TextPage ToTextPage(Size maxSize, char fill)
        {
            throw new NotImplementedException();
        }
    }
}
