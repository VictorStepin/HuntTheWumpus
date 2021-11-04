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
        private bool _wumpusRevealed;

        private string[] _messages;

        public Game ()
        {
            Random rng = new Random();

            _maze = new Maze(MAZE_DIMENSION);

            Location playerStartLocation = new Location(rng.Next(0, _maze.Cells.GetLength(0)),
                                                        rng.Next(0, _maze.Cells.GetLength(1)));
            _player = new Player(playerStartLocation);

            Location wumpusStartLocation = new Location(rng.Next(0, _maze.Cells.GetLength(0)),
                                                        rng.Next(0, _maze.Cells.GetLength(1)));
            while (wumpusStartLocation == playerStartLocation || TwoObjectsLocationsNearby(playerStartLocation, wumpusStartLocation))
            {
                wumpusStartLocation = new Location(rng.Next(0, _maze.Cells.GetLength(0)),
                                                   rng.Next(0, _maze.Cells.GetLength(1)));
            }
            _wumpus = new Wumpus(wumpusStartLocation);
            _wumpusRevealed = false;

            _messages = new string[MESSAGES_MAX_COUNT];
        }

        public void Run ()
        {
            while (_player.IsAlive && _wumpus.IsAlive)
            {
                UpdateMaze();
                ClearMessages();

                ConsoleKey actionKey = Console.ReadKey(true).Key;
                PerformPlayerAction(actionKey);

                Direction wumpusDirection = (Direction)new Random().Next(0, 4);
                PerformWumpusMove(wumpusDirection);

                if (TwoObjectsLocationsNearby(_player.GetLocation(), _wumpus.GetLocation()))
                {
                    AddMessage("�� ���������� �������������� �����..");
                }

                if (WumpusAtePlayer())
                {
                    _wumpusRevealed = true;
                    AddMessage("������ ���� ���!");
                    UpdateMaze();
                    _player.IsAlive = false;
                }
            }

            if (!_player.IsAlive)
            {
                AddMessage("���� ���������.");
            }
            else if (!_wumpus.IsAlive)
            {
                AddMessage("�� ����� �������. ������!");
            }
            UpdateMaze();

        }

        private void UpdateMaze ()
        {
            Console.Clear();
            _maze.Clear();

            _maze.Cells[_player.GetLocation().X, _player.GetLocation().Y].Content = CellContent.Player;
            _maze.Cells[_wumpus.GetLocation().X, _wumpus.GetLocation().Y].Content = CellContent.Wumpus;

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
                            if (_wumpusRevealed)
                            {
                                Console.Write("[");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("W");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("]");
                            }
                            else
                            {
                                Console.Write("[ ]");
                            }
                            break;
                    }
                }
                Console.WriteLine();
            }

            PrintMessages();

            Console.WriteLine("\nX - �����");
        }

        private void PerformPlayerAction (ConsoleKey actionKey)
        {
            switch (actionKey)
            {
                case ConsoleKey.W:
                    if (_player.GetLocation().Y != 0)
                    {
                        _player.Move(Direction.Up);
                    }
                    break;
                case ConsoleKey.A:
                    if (_player.GetLocation().X != 0)
                    {
                        _player.Move(Direction.Left);
                    }
                    break;
                case ConsoleKey.S:
                    if (_player.GetLocation().Y != _maze.Cells.GetLength(1) - 1)
                    {
                        _player.Move(Direction.Down);
                    }
                    break;
                case ConsoleKey.D:
                    if (_player.GetLocation().X != _maze.Cells.GetLength(0) - 1)
                    {
                        _player.Move(Direction.Right);
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (_wumpus.GetLocation().X == _player.GetLocation().X &&
                        _wumpus.GetLocation().Y == _player.GetLocation().Y - 1)
                    {
                        _wumpusRevealed = true;
                        _wumpus.IsAlive = false;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (_wumpus.GetLocation().X == _player.GetLocation().X - 1 &&
                        _wumpus.GetLocation().Y == _player.GetLocation().Y)
                    {
                        _wumpusRevealed = true;
                        _wumpus.IsAlive = false;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (_wumpus.GetLocation().X == _player.GetLocation().X &&
                        _wumpus.GetLocation().Y == _player.GetLocation().Y + 1)
                    {
                        _wumpusRevealed = true;
                        _wumpus.IsAlive = false;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (_wumpus.GetLocation().X == _player.GetLocation().X + 1 &&
                        _wumpus.GetLocation().Y == _player.GetLocation().Y)
                    {
                        _wumpusRevealed = true;
                        _wumpus.IsAlive = false;
                    }
                    break;
                case ConsoleKey.X:
                    _player.IsAlive = false;
                    break;
            }
        }

        private void PerformWumpusMove (Direction wumpusDirection)
        {
            int moveProbability = new Random().Next(0, 100);
            if (moveProbability <= 25 && _wumpus.IsAlive)
            {
                switch (wumpusDirection)
                {
                    case Direction.Up:
                        if (_wumpus.GetLocation().Y != 0)
                        {
                            _wumpus.Move(wumpusDirection);
                        }
                        break;
                    case Direction.Right:
                        if (_wumpus.GetLocation().X != _maze.Cells.GetLength(0) - 1)
                        {
                            _wumpus.Move(wumpusDirection);
                        }
                        break;
                    case Direction.Down:
                        if (_wumpus.GetLocation().Y != _maze.Cells.GetLength(1) - 1)
                        {
                            _wumpus.Move(wumpusDirection);
                        }
                        break;
                    case Direction.Left:
                        if (_wumpus.GetLocation().X != 0)
                        {
                            _wumpus.Move(wumpusDirection);
                        }
                        break;
                }
            }
        }

        private bool WumpusAtePlayer ()
        {
            return _player.GetLocation() == _wumpus.GetLocation();
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
