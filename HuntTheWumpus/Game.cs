using System;

namespace HuntTheWumpus
{
    class Game
    {
        private bool _isRunning;

        private Maze _maze;
        private Player _player;

        public Game ()
        {
            _maze = new Maze(6);
            _player = new Player(new Location(0, 0));

            _isRunning = true;
        }

        public void Run ()
        {
            while (_isRunning)
            {
                UpdateMaze();

                ConsoleKey inputKey = Console.ReadKey(true).Key;

                switch (inputKey)
                {
                    case ConsoleKey.W:
                        if (_player.Location.Y != 0)
                        {
                            _player.Move(Direction.Up);
                        }
                        break;
                    case ConsoleKey.A:
                        if (_player.Location.X != 0)
                        {
                            _player.Move(Direction.Left);
                        }
                        break;
                    case ConsoleKey.S:
                        if (_player.Location.Y != _maze.Cells.GetLength(1) - 1)
                        {
                            _player.Move(Direction.Down);
                        }
                        break;
                    case ConsoleKey.D:
                        if (_player.Location.X != _maze.Cells.GetLength(0) - 1)
                        {
                            _player.Move(Direction.Right);
                        }
                        break;
                    case ConsoleKey.X:
                        Console.Clear();
                        _isRunning = false;
                        break;
                }
            }
        }

        private void UpdateMaze ()
        {
            Console.Clear();
            
            _maze.Clear();

            Cell[,] mazeCells = _maze.Cells;
            mazeCells[_player.Location.X, _player.Location.Y].Content = CellContent.Player;


            for (int y = 0; y < mazeCells.GetLength(1); y++) 
            {
                for (int x = 0; x < mazeCells.GetLength(0); x++)
                {
                    Cell cell = mazeCells[x, y];

                    switch (cell.Content)
                    {
                        case CellContent.Empty:
                            Console.Write("[ ]");
                            break;
                        case CellContent.Player:
                            Console.Write("[@]");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}