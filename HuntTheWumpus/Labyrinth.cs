namespace HuntTheWumpus
{
    class Labyrinth
    {
        private const int CELLS_COUNT = 6;

        public Cell[] Cells { get; private set; }

        public Labyrinth ()
        {
            Cells = new Cell[CELLS_COUNT];
            for (int i = 0; i < Cells.Length; i++)
            {
                Cell cell = new Cell(CellContent.Empty);
                Cells[i] = cell;
            }
        }

        public void ClearCells ()
        {
            foreach (Cell cell in Cells)
            {
                cell.Content = CellContent.Empty;
            }
        }
    }
}
