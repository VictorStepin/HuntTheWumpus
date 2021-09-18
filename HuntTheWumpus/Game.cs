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

                // Ход игрока
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

                // Ход Вампуса
                Direction wumpusDirection = (Direction)new Random().Next(0, 5);
                _wumpus.Move(wumpusDirection);

                // Проверка, не находятся ли игрок и Вампус на одной клетке
                if (_player.Location.X == _wumpus.Location.X &&
                    _player.Location.Y == _wumpus.Location.Y)
                {
                    UpdateMaze();
                    Console.WriteLine("Вампус съел тебя!");
                    Console.WriteLine("Игра закончена.");
                    break;
                }
            }
        }

        private void UpdateMaze ()
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
        }
    }
}
