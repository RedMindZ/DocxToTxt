using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementTableBuilder
    {
        private readonly HashSet<CellInfo> _cells;
        private readonly TextElementTableRow[] _rows;
        private readonly TextElementTableColumn[] _columns;

        public int Height { get; }
        public int Width { get; }



        public TextElementTableBuilder(int height, int width)
        {
            _cells = new HashSet<CellInfo>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _cells.Add(new CellInfo(i, j, 1, 1));
                }
            }

            _rows = new TextElementTableRow[height];

            for (int i = 0; i < _rows.Length; i++)
            {
                _rows[i] = new TextElementTableRow();
            }

            _columns = new TextElementTableColumn[width];

            for (int i = 0; i < _columns.Length; i++)
            {
                _columns[i] = new TextElementTableColumn();
            }

            Height = height;
            Width = width;
        }



        public void TryMergeCells(CellInfo cell, out bool merged)
        {
            List<CellInfo> intersections = new List<CellInfo>();

            foreach (CellInfo c in _cells)
            {
                if (Intersect(cell, c))
                {
                    if (!IsSuperCell(cell, c))
                    {
                        merged = false;
                        return;
                    }
                    else
                    {
                        intersections.Add(c);
                    }
                }
            }

            _cells.ExceptWith(intersections);
            _cells.Add(new CellInfo(cell));

            merged = true;
        }

        private static bool IsSuperCell(CellInfo superCell, CellInfo subCell)
        {
            return subCell.Index.Row >= superCell.Index.Row && subCell.Index.Column >= subCell.Index.Column &&
                   superCell.Span.Height >= subCell.Span.Height && superCell.Span.Width >= subCell.Span.Width;
        }

        private static bool Intersect(CellInfo c1, CellInfo c2)
        {
            return (c1.Index.Row <= c2.Index.Row + c2.Span.Height - 1) && (c2.Index.Row <= c1.Index.Row + c1.Span.Height - 1) &&
                   (c1.Index.Column <= c2.Index.Column + c2.Span.Width - 1) && (c2.Index.Column <= c1.Index.Column + c1.Span.Width - 1);
        }



        public void SetRowHeight(int rowIndex, int height)
        {
            if (rowIndex < 0 || rowIndex >= _rows.Length)
            {
                throw new IndexOutOfRangeException($"{nameof(rowIndex)} must be non-negative and less then the number of rows.");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), $"{nameof(height)} must be non-negative.");
            }

            _rows[rowIndex].Height = height;
        }

        public void SetColumnWidth(int columnIndex, int width)
        {
            if (columnIndex < 0 || columnIndex >= _columns.Length)
            {
                throw new IndexOutOfRangeException($"{nameof(columnIndex)} must be non-negative and less then the number of columns.");
            }

            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), $"{nameof(width)} must be non-negative.");
            }

            _columns[columnIndex].Width = width;
        }
    }
}
