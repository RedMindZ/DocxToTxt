using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class Size
    {
        public static Size MaxSize => new Size(int.MaxValue, int.MaxValue);

        public int Height { get; set; }
        public int Width { get; set; }



        public Size() { }

        public Size(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public Size(Size other)
        {
            Height = other.Height;
            Width = other.Width;
        }

        public static Size MinPositiveSize(Size s1, Size s2)
        {
            return new Size
            {
                Height = MathUtils.Clamp(s1.Height, 0, s2.Height),
                Width = MathUtils.Clamp(s1.Width, 0, s2.Width)
            };
        }
    }
}
