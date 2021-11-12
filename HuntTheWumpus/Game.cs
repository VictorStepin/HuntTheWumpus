using System;

namespace HuntTheWumpus
{
    class Game
    {
        private const int MAZE_DIMENSION = 6;

        private Maze _maze;
        private Player _player;
        private Wumpus _wumpus;
        private bool _wumpusRevealed;


        public Game()
        {
            Random rng = new Random();

            Location playerStartLocation = new Location(rng.Next(0, MAZE_DIMENSION),
                                                        rng.Next(0, MAZE_DIMENSION));
            _player = new Player(playerStartLocation);

            Location wumpusStartLocation = new Location(rng.Next(0, MAZE_DIMENSION),
                                                        rng.Next(0, MAZE_DIMENSION));
            while (wumpusStartLocation == playerStartLocation || TwoObjectsLocateNearby(playerStartLocation, wumpusStartLocation))
            {
                wumpusStartLocation = new Location(rng.Next(0, MAZE_DIMENSION),
                                                   rng.Next(0, MAZE_DIMENSION));
            }
            _wumpus = new Wumpus(wumpusStartLocation);
            _wumpusRevealed = false;

            GameObject[] gameObjects = new GameObject[]
            {
                _player,
                _wumpus
            };

            _maze = new Maze(MAZE_DIMENSION, gameObjects);
        }

        public void Run()
        {
            while (_player.IsAlive && _wumpus.IsAlive)
            {
                _maze.Update();
                Render();
                Messenger.ClearMessages();

                ConsoleKey actionKey = Console.ReadKey(true).Key;
                PerformPlayerAction(actionKey);

                Direction wumpusDirection = (Direction)new Random().Next(0, 4);
                PerformWumpusMove(wumpusDirection);

                if (TwoObjectsLocateNearby(_player.GetLocation(), _wumpus.GetLocation()))
                {
                    Messenger.AddMessage("Вы чувствуете отвратительный запах..");
                }

                if (WumpusAtePlayer())
                {
                    _wumpusRevealed = true;
                    Messenger.AddMessage("Вампус съел вас!");
                    _maze.Update();
                    Render();
                    _player.IsAlive = false;
                }
            }

            if (!_player.IsAlive)
            {
                Messenger.AddMessage("Игра закончена.");
            }
            else if (!_wumpus.IsAlive)
            {
                Messenger.AddMessage("Вы убили Вампуса. Победа!");
            }

            _maze.Update();
            Render();
        }

        /// <summary>
        /// Renders maze and prints messages to console.
        /// </summary>
        private void Render()
        {
            Console.Clear();

            for (int y = 0; y < MAZE_DIMENSION; y++)
            {
                for (int x = 0; x < MAZE_DIMENSION; x++)
                {
                    Cell cell = _maze.GetCells()[x, y];

                    switch (cell.GetContent())
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

            Console.WriteLine("\nX - выход");
        }

        private void PrintMessages()
        {
            string[] messages = Messenger.GetMessages();

            for (int i = 0; i < messages.Length; i++)
            {
                if (messages[i] != null)
                {
                    Console.WriteLine(messages[i]);
                }
            }
        }

        private void PerformPlayerAction(ConsoleKey actionKey)
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
                    if (_player.GetLocation().Y != MAZE_DIMENSION - 1)
                    {
                        _player.Move(Direction.Down);
                    }
                    break;
                case ConsoleKey.D:
                    if (_player.GetLocation().X != MAZE_DIMENSION - 1)
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

        private void PerformWumpusMove(Direction wumpusDirection)
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
                        if (_wumpus.GetLocation().X != MAZE_DIMENSION - 1)
                        {
                            _wumpus.Move(wumpusDirection);
                        }
                        break;
                    case Direction.Down:
                        if (_wumpus.GetLocation().Y != MAZE_DIMENSION - 1)
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

        private bool WumpusAtePlayer()
        {
            return _player.GetLocation() == _wumpus.GetLocation();
        }

        private bool TwoObjectsLocateNearby(Location ol1, Location ol2)
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
    }
}
