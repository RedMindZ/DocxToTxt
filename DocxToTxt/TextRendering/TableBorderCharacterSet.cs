using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TableBorderCharacterSet
    {
        private char[,,,] _charTable = new char[2, 2, 2, 2];

        public char TopLeftCorner { get => _charTable[0, 0, 1, 1]; set => _charTable[0, 0, 1, 1] = value; }
        public char TopRightCorner { get => _charTable[0, 1, 1, 0]; set => _charTable[0, 1, 1, 0] = value; }
        public char BottomLeftCorner { get => _charTable[1, 0, 0, 1]; set => _charTable[1, 0, 0, 1] = value; }
        public char BottomRightCorner { get => _charTable[1, 1, 0, 0]; set => _charTable[1, 1, 0, 0] = value; }

        public char TopJunction { get => _charTable[0, 1, 1, 1]; set => _charTable[0, 1, 1, 1] = value; }
        public char LeftJunction { get => _charTable[1, 0, 1, 1]; set => _charTable[1, 0, 1, 1] = value; }
        public char BottomJunction { get => _charTable[1, 1, 0, 1]; set => _charTable[1, 1, 0, 1] = value; }
        public char RightJunction { get => _charTable[1, 1, 1, 0]; set => _charTable[1, 1, 1, 0] = value; }

        public char HorizontalConnector { get => _charTable[0, 1, 0, 1]; set => _charTable[0, 1, 0, 1] = value; }
        public char VerticalConnector { get => _charTable[1, 0, 1, 0]; set => _charTable[1, 0, 1, 0] = value; }

        public char FullJunction { get => _charTable[1, 1, 1, 1]; set => _charTable[1, 1, 1, 1] = value; }



        public TableBorderCharacterSet()
        {
            //        [T, L, B, R]
            _charTable[0, 0, 0, 0] = ' ';
            _charTable[0, 0, 0, 1] = ' ';
            _charTable[0, 0, 1, 0] = ' ';
            _charTable[0, 0, 1, 1] = '┌';
            _charTable[0, 1, 0, 0] = ' ';
            _charTable[0, 1, 0, 1] = '─';
            _charTable[0, 1, 1, 0] = '┐';
            _charTable[0, 1, 1, 1] = '┬';
            _charTable[1, 0, 0, 0] = ' ';
            _charTable[1, 0, 0, 1] = '└';
            _charTable[1, 0, 1, 0] = '│';
            _charTable[1, 0, 1, 1] = '├';
            _charTable[1, 1, 0, 0] = '┘';
            _charTable[1, 1, 0, 1] = '┴';
            _charTable[1, 1, 1, 0] = '┤';
            _charTable[1, 1, 1, 1] = '┼';
        }

        public char this[int top, int left, int bottom, int right]
        {
            get => _charTable[top, left, bottom, right];
            set => _charTable[top, left, bottom, right] = value;
        }

        public char this[bool top, bool left, bool bottom, bool right]
        {
            get => this[Convert.ToInt32(top), Convert.ToInt32(left), Convert.ToInt32(bottom), Convert.ToInt32(right)];
            set => this[Convert.ToInt32(top), Convert.ToInt32(left), Convert.ToInt32(bottom), Convert.ToInt32(right)] = value;
        }



        public static TableBorderCharacterSet EmptySet
        {
            get
            {
                TableBorderCharacterSet characterSet = new TableBorderCharacterSet();
                characterSet._charTable[0, 0, 0, 0] = ' ';
                characterSet._charTable[0, 0, 0, 1] = ' ';
                characterSet._charTable[0, 0, 1, 0] = ' ';
                characterSet._charTable[0, 0, 1, 1] = ' ';
                characterSet._charTable[0, 1, 0, 0] = ' ';
                characterSet._charTable[0, 1, 0, 1] = ' ';
                characterSet._charTable[0, 1, 1, 0] = ' ';
                characterSet._charTable[0, 1, 1, 1] = ' ';
                characterSet._charTable[1, 0, 0, 0] = ' ';
                characterSet._charTable[1, 0, 0, 1] = ' ';
                characterSet._charTable[1, 0, 1, 0] = ' ';
                characterSet._charTable[1, 0, 1, 1] = ' ';
                characterSet._charTable[1, 1, 0, 0] = ' ';
                characterSet._charTable[1, 1, 0, 1] = ' ';
                characterSet._charTable[1, 1, 1, 0] = ' ';
                characterSet._charTable[1, 1, 1, 1] = ' ';

                return characterSet;
            }
        }
    }
}
