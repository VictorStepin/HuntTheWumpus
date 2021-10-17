using System;

namespace HuntTheWumpus
{
    class Game
    {
        private const int MAZE_DIMENSION = 6;

        private Maze _maze;
        private Player _player;
        private Wumpus _wumpus;

        private bool _isRunning;

        public Game ()
        {
            _maze = new Maze(MAZE_DIMENSION);
            _player = new Player(new Location(1, 2));
            _wumpus = new Wumpus(new Location(4, 3));

            _isRunning = true;
        }

        public void Run ()
        {
            while (_isRunning)
            {
                UpdateMaze();

                ConsoleKey actionKey = Console.ReadKey(true).Key;
                PerformPlayerAction(actionKey);

                Direction wumpusDirection = (Direction)new Random().Next(0, 4);
                PerformWumpusMove(wumpusDirection);

                if (IsGameOver())
                {
                    UpdateMaze();
                    Console.WriteLine("\nВампус съел тебя!");
                    Console.WriteLine("Игра закончена.");
                    _isRunning = false;
                }
            }
        }

        private void UpdateMaze()
        {
            Console.Clear();

            _maze.Clear();

            _maze.Cells[_player.Location.X, _player.Location.Y].Content = CellContent.Player;
            _maze.Cells[_wumpus.Location.X, _wumpus.Location.Y].Content = CellContent.Wumpus;


            for (int y = 0; y < _maze.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < _maze.Cells.GetLength(0); x++)
                {
                    Cell cell = _maze.Cells[x, y];

                    switch (cell.Content)
                    {
                        case CellContent.Empty:
                            Console.Write("[ ]");
                            break;
                        case CellContent.Player:
                            Console.Write("[@]");
                            break;
                        case CellContent.Wumpus:
                            Console.Write("[W]");
                            break;
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("X - выход");
        }

        private void PerformPlayerAction(ConsoleKey actionKey)
        {
            switch (actionKey)
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

        private void PerformWumpusMove(Direction wumpusDirection)
        {
            switch (wumpusDirection)
            {
                case Direction.Up:
                    if (_wumpus.Location.Y != 0)
                    {
                        _wumpus.Move(wumpusDirection);
                    }
                    break;
                case Direction.Right:
                    if (_wumpus.Location.X != _maze.Cells.GetLength(0) - 1)
                    {
                        _wumpus.Move(wumpusDirection);
                    }
                    break;
                case Direction.Down:
                    if (_wumpus.Location.Y != _maze.Cells.GetLength(1) - 1)
                    {
                        _wumpus.Move(wumpusDirection);
                    }
                    break;
                case Direction.Left:
                    if (_wumpus.Location.X != 0)
                    {
                        _wumpus.Move(wumpusDirection);
                    }
                    break;
            }
        }

        private bool IsGameOver()
        {
            return _player.Location.X == _wumpus.Location.X && _player.Location.Y == _wumpus.Location.Y;
        }
    }
}
