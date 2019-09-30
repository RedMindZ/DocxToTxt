using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementTable : ITextElement
    {
        public Size DesiredSize { get; protected set; }

        private readonly HashSet<TextElementTableCell> _cells;
        private readonly TextElementTableRow[] _rows;
        private readonly TextElementTableColumn[] _columns;

        public int Height => _rows.Length;
        public int Width => _columns.Length;

        public TableBorderCharacterSet BorderCharacterSet { get; set; } = new TableBorderCharacterSet();



        public TextElementTable(int height, int width)
        {
            if (height < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(height), $"{nameof(height)} must be a positive integer.");
            }

            if (width < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(width), $"{nameof(width)} must be a positive integer.");
            }

            _cells = new HashSet<TextElementTableCell>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _cells.Add(new TextElementTableCell() { TableRectangle = new Rectangle(i, j, 1, 1) });
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
        }



        public void TryMergeCells(Rectangle mergeRect, out bool merged)
        {
            List<TextElementTableCell> intersections = new List<TextElementTableCell>();

            foreach (TextElementTableCell cell in _cells)
            {
                if (Rectangle.TestIntersection(mergeRect, cell.TableRectangle))
                {
                    if (!Rectangle.TestSuperRectangle(mergeRect, cell.TableRectangle))
                    {
                        merged = false;
                        return;
                    }
                    else
                    {
                        intersections.Add(cell);
                    }
                }
            }

            _cells.ExceptWith(intersections);
            _cells.Add(new TextElementTableCell() { TableRectangle = new Rectangle(mergeRect) });

            merged = true;
        }



        public void SetRowHeight(int height, int rowIndex)
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

        public void SetRowHeight(int uniformHeight)
        {
            if (uniformHeight < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(uniformHeight), $"{nameof(uniformHeight)} must be non-negative.");
            }

            for (int i = 0; i < _rows.Length; i++)
            {
                _rows[i].Height = uniformHeight; 
            }
        }

        public void SetColumnWidth(int width, int columnIndex)
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

        public void SetColumnWidth(int uniformWidth)
        {
            if (uniformWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(uniformWidth), $"{nameof(uniformWidth)} must be non-negative.");
            }

            for (int i = 0; i < _columns.Length; i++)
            {
                _columns[i].Width = uniformWidth; 
            }
        }

        public void SetCellPadding(Padding padding, Index2D cellIndex)
        {
            TextElementTableCell cell = FindCell(cellIndex) ?? throw new ArgumentException($"The {nameof(cellIndex)} specified did not match any cells.", nameof(cellIndex));

            cell.CellPadding = padding;
        }

        public void SetCellChild(ITextElement child, Index2D cellIndex)
        {
            TextElementTableCell cell = FindCell(cellIndex) ?? throw new ArgumentException($"The {nameof(cellIndex)} specified did not match any cells.", nameof(cellIndex));

            cell.Child = child;
        }

        private TextElementTableCell FindCell(Index2D cellIndex)
        {
            Rectangle cellRect = new Rectangle(cellIndex.Row, cellIndex.Column, 1, 1);

            foreach (TextElementTableCell cell in _cells)
            {
                if (Rectangle.TestIntersection(cellRect, cell.TableRectangle))
                {
                    return cell;
                }
            }

            return null;
        }



        public void Measure(Size maxSize)
        {
            foreach (TextElementTableCell cell in _cells)
            {
                cell.CellSize = ResolveCellSize(cell.TableRectangle);
                cell.Measure(cell.CellSize);
            }

            DesiredSize = new Size
            (
                _rows.Sum(r => r.Height) + (_rows.Length * 1 + 1),
                _columns.Sum(c => c.Width) + (_columns.Length * 1 + 1)
            );
        }

        private Size ResolveCellSize(Rectangle cellRect)
        {
            Size cellSize = new Size(0, 0);

            for (int i = 0; i < cellRect.Height; i++)
            {
                cellSize.Height += _rows[cellRect.Y + i].Height;

                if (i + 1 < cellRect.Height)
                {
                    cellSize.Height += 1;
                }
            }

            for (int i = 0; i < cellRect.Width; i++)
            {
                cellSize.Width += _columns[cellRect.X + i].Width;

                if (i + 1 < cellRect.Width)
                {
                    cellSize.Width += 1;
                }
            }

            return cellSize;
        }



        public TextPage ToTextPage(Size maxSize, char fill)
        {
            TextPage page = new TextPage(DesiredSize.Height, DesiredSize.Width, fill);

            bool[,] occupiedEntries = new bool[DesiredSize.Height, DesiredSize.Width];

            foreach (TextElementTableCell cell in _cells)
            {
                Index2D cellPosition = ResolveCellPosition(cell.TableRectangle);
                TextPage cellPage = cell.ToTextPage(cell.DesiredSize, fill);

                TextPage.Blit(cellPage, page, cellPosition.Column, cellPosition.Row);

                for (int i = 0; i < cell.CellSize.Height; i++)
                {
                    for (int j = 0; j < cell.CellSize.Width; j++)
                    {
                        occupiedEntries[cellPosition.Row + i, cellPosition.Column + j] = true;
                    }
                }
            }

            for (int i = 0; i < page.LineCount; i++)
            {
                for (int j = 0; j < page.LineLength; j++)
                {
                    if (!occupiedEntries[i, j])
                    {
                        page[i, j] = ResolveBorderCharacter(occupiedEntries, i, j);
                    }
                }
            }

            return page;
        }

        private Index2D ResolveCellPosition(Rectangle cellRect)
        {
            Index2D cellIndex = new Index2D(1, 1);

            for (int i = 0; i < cellRect.Y; i++)
            {
                cellIndex.Row += _rows[i].Height + 1;
            }

            for (int i = 0; i < cellRect.X; i++)
            {
                cellIndex.Column += _columns[i].Width + 1;
            }

            return cellIndex;
        }

        private char ResolveBorderCharacter(bool[,] occupiedEntries, int row, int column)
        {
            return BorderCharacterSet
            [
                 row > 0 ? !occupiedEntries[row - 1, column] : false,
                 column > 0 ? !occupiedEntries[row, column - 1] : false,
                 row < occupiedEntries.GetLength(0) - 1 ? !occupiedEntries[row + 1, column] : false,
                 column < occupiedEntries.GetLength(1) - 1 ? !occupiedEntries[row, column + 1] : false
            ];
        }
    }
}
