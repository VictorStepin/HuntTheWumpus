namespace HuntTheWumpus
{
    class Maze
    {
        public Cell[,] Cells { get; }

        public Maze (int dimension)
        {
            Cells = new Cell[dimension, dimension];
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    Cell cell = new Cell(CellContent.Empty);
                    Cells[x, y] = cell;
                }
            }
        }

        public void Clear ()
        {
            foreach (Cell cell in Cells)
            {
                cell.Content = CellContent.Empty;
            }
        }
    }
}
