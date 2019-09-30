namespace DocxToTxt.TextRendering
{
    public class Padding
    {
        public int Top { get; set; } = 0;
        public int Left { get; set; } = 0;
        public int Bottom { get; set; } = 0;
        public int Right { get; set; } = 0;



        public Padding() { }

        public Padding(int uniformPad) : this(uniformPad, uniformPad, uniformPad, uniformPad) { }

        public Padding(int top, int left, int bottom, int right)
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }
    }
}