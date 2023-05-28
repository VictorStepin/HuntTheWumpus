namespace HuntTheWumpus
{
    internal class Maze
    {
        private MazeObject[] _gameObjects;
        private Cell[,] _cells;

        public Maze (int dimension, MazeObject[] gameObjects)
        {
            _gameObjects = gameObjects;

            _cells = (new Cell[dimension, dimension]);
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    Cell cell = new Cell(CellContent.Empty);
                    _cells[x, y] = cell;
                }
            }
        }

        public Cell[,] GetCells()
        {
            return _cells;
        }

        /// <summary>
        /// Updates maze's state.
        /// </summary>
        public void Update()
        {
            Clear();
            _cells[_gameObjects[0].GetLocation().X, _gameObjects[0].GetLocation().Y].SetContent(CellContent.Player);
            _cells[_gameObjects[1].GetLocation().X, _gameObjects[1].GetLocation().Y].SetContent(CellContent.Wumpus);
            _cells[_gameObjects[2].GetLocation().X, _gameObjects[2].GetLocation().Y].SetContent(CellContent.Pit);
            _cells[_gameObjects[3].GetLocation().X, _gameObjects[3].GetLocation().Y].SetContent(CellContent.Pit);
        }

        private void Clear ()
        {
            foreach (Cell cell in _cells)
            {
                cell.SetContent(CellContent.Empty);
            }
        }
    }
}
