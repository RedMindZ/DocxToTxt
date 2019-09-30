using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class Point
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;



        public Point() { }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point other)
        {
            X = other.X;
            Y = other.Y;
        }
    }
}
