using System;

namespace HuntTheWumpus
{
    class Game
    {
        private const int MAZE_DIMENSION = 6;
        private const int MESSAGES_MAX_COUNT = 5;

        private Maze _maze;
        private Player _player;
        private Wumpus _wumpus;

        private string[] _messages;

        public Game ()
        {
            _maze = new Maze(MAZE_DIMENSION);
            _player = new Player(new Location(1, 2));
            _wumpus = new Wumpus(new Location(4, 3));

            _messages = new string[MESSAGES_MAX_COUNT];
        }

        public void Run ()
        {
            while (_player.Alive && _wumpus.Alive)
            {
                UpdateMaze();
                ClearMessages();

                ConsoleKey actionKey = Console.ReadKey(true).Key;
                PerformPlayerAction(actionKey);

                Direction wumpusDirection = (Direction)new Random().Next(0, 4);
                PerformWumpusMove(wumpusDirection);

                if (TwoObjectsLocationsNearby(_player.Location, _wumpus.Location))
                {
                    AddMessage("Вы чувствуете отвратительный запах..");
                }

                if (WumpusAtePlayer())
                {
                    UpdateMaze();
                    _player.Alive = false;
                }
            }

            if (!_player.Alive)
            {
                AddMessage("Игра закончена.");
            }
            else if (!_wumpus.Alive)
            {
                AddMessage("Вы убили Вампуса. Победа!");
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
                            Console.Write("[");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("@");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("]");
                            break;
                        case CellContent.Wumpus:
                            Console.Write("[");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("W");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("]");
                            break;
                    }
                }
                Console.WriteLine();
            }

            PrintMessages();

            Console.WriteLine("\nX - выход");
        }

        private void PerformPlayerAction (ConsoleKey actionKey)
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
                case ConsoleKey.UpArrow:
                    if (_wumpus.Location.X == _player.Location.X &&
                        _wumpus.Location.Y == _player.Location.Y - 1)
                    {
                        _wumpus.Alive = false;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (_wumpus.Location.X == _player.Location.X - 1 &&
                        _wumpus.Location.Y == _player.Location.Y)
                    {
                        _wumpus.Alive = false;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (_wumpus.Location.X == _player.Location.X &&
                        _wumpus.Location.Y == _player.Location.Y + 1)
                    {
                        _wumpus.Alive = false;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (_wumpus.Location.X == _player.Location.X + 1 &&
                        _wumpus.Location.Y == _player.Location.Y)
                    {
                        _wumpus.Alive = false;
                    }
                    break;
                case ConsoleKey.X:
                    _player.Alive = false;
                    break;
            }
        }

        private void PerformWumpusMove (Direction wumpusDirection)
        {
            int moveProbability = new Random().Next(0, 100);
            if (moveProbability <= 25)
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
        }

        private bool WumpusAtePlayer ()
        {
            return _player.Location.X == _wumpus.Location.X && _player.Location.Y == _wumpus.Location.Y;
        }

        private bool TwoObjectsLocationsNearby (Location ol1, Location ol2)
        {
            if (ol1.X == ol2.X && ol1.Y == ol2.Y + 1 ||
                ol1.X == ol2.X - 1 && ol1.Y == ol2.Y + 1 ||
                ol1.X == ol2.X - 1 && ol1.Y == ol2.Y ||
                ol1.X == ol2.X - 1 && ol1.Y == ol2.Y - 1 ||
                ol1.X == ol2.X && ol1.Y == ol2.Y - 1 ||
                ol1.X == ol2.X + 1 && ol1.Y == ol2.Y - 1 ||
                ol1.X == ol2.X + 1 && ol1.Y == ol2.Y ||
                ol1.X == ol2.X + 1 && ol1.Y == ol2.Y + 1)
            {
                return true;
            }
            
            return false;
        }

        private void ClearMessages ()
        {
            for (int i = 0; i < _messages.Length; i++)
            {
                _messages[i] = null;
            }
        }

        private void AddMessage (string message)
        {
            bool messageAdded = false;
            for (int i = 0; i < _messages.Length; i++)
            {
                if (_messages[i] == null)
                {
                    _messages[i] = message;
                    messageAdded = true;
                    break;
                }
            }

            if (!messageAdded)
            {
                Console.WriteLine("ERROR!!!!!!!");
            }
        }

        private void PrintMessages ()
        {
            for (int i = 0; i < _messages.Length; i++)
            {
                if (_messages[i] != null)
                {
                    Console.WriteLine(_messages[i]);
                }
            }
        }
    }
}
