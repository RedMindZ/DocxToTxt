using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class Rectangle
    {
        public int Y { get; set; }
        public int X { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }



        public Rectangle() { }

        public Rectangle(Point p1, Point p2)
        {
            X = Math.Min(p1.X, p2.X);
            Y = Math.Min(p1.Y, p2.Y);
            Width = Math.Abs(p1.X - p2.X);
            Height = Math.Abs(p1.Y - p2.Y);
        }

        public Rectangle(int y, int x, int height, int width)
        {
            Y = y;
            X = x;
            Height = height;
            Width = width;
        }

        public Rectangle(Rectangle other)
        {
            Y = other.Y;
            X = other.X;
            Height = other.Height;
            Width = other.Width;
        }



        public static bool TestIntersection(Rectangle r1, Rectangle r2)
        {
            return (r1.Y < r2.Y + r2.Height) &&
                   (r2.Y < r1.Y + r1.Height) &&
                   (r1.X < r2.X + r2.Width) &&
                   (r2.X < r1.X + r1.Width);
        }

        public static bool TestSuperRectangle(Rectangle superRect, Rectangle subRect)
        {
            return (subRect.Y >= superRect.Y) &&
                   (subRect.X >= superRect.X) &&
                   (superRect.Y + superRect.Height >= subRect.Y + subRect.Height) &&
                   (superRect.X + superRect.Width >= subRect.X + subRect.Width);
        }
    }
}
