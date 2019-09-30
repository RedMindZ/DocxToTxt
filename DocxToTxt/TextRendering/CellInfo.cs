namespace DocxToTxt.TextRendering
{
    public class CellInfo
    {
        public Index2D Index { get; set; } = new Index2D(0, 0);
        public Size Span { get; set; } = new Size(1, 1);

        public CellInfo() { }
        public CellInfo(Index2D index, Size span) : this(index.Row, index.Column, span.Height, span.Width) { }
        public CellInfo(CellInfo other) : this(other.Index.Row, other.Index.Column, other.Span.Height, other.Span.Width) { }
        public CellInfo(int row, int column, int spanHeight, int spanWidth)
        {
            Index = new Index2D(row, column);
            Span = new Size(spanHeight, spanWidth);
        }
    }
}
