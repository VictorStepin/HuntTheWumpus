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
        private Pit _pit1;
        private bool _pit1Revealed;
        private Pit _pit2;
        private bool _pit2Revealed;


        public Game()
        {
            Location playerStartLocation = new Location(RNG.NumberBetween(0, MAZE_DIMENSION),
                                                        RNG.NumberBetween(0, MAZE_DIMENSION));
            _player = new Player(playerStartLocation);

            Location wumpusStartLocation = new Location(RNG.NumberBetween(0, MAZE_DIMENSION),
                                                        RNG.NumberBetween(0, MAZE_DIMENSION));
            while (wumpusStartLocation == playerStartLocation || TwoObjectsLocateNearby(playerStartLocation, wumpusStartLocation))
            {
                wumpusStartLocation = new Location(RNG.NumberBetween(0, MAZE_DIMENSION),
                                                   RNG.NumberBetween(0, MAZE_DIMENSION));
            }
            _wumpus = new Wumpus(wumpusStartLocation);
            _wumpusRevealed = false;

            Location pit1StartLocation = new Location(RNG.NumberBetween(0, MAZE_DIMENSION),
                                                      RNG.NumberBetween(0, MAZE_DIMENSION));
            while (pit1StartLocation == playerStartLocation || 
                   pit1StartLocation == wumpusStartLocation ||
                   TwoObjectsLocateNearby(playerStartLocation, pit1StartLocation))
            {
                pit1StartLocation = new Location(RNG.NumberBetween(0, MAZE_DIMENSION),
                                                 RNG.NumberBetween(0, MAZE_DIMENSION));
            }
            _pit1 = new Pit(pit1StartLocation);
            _pit1Revealed = false;

            Location pit2StartLocation = new Location(RNG.NumberBetween(0, MAZE_DIMENSION),
                                                      RNG.NumberBetween(0, MAZE_DIMENSION));
            while (pit2StartLocation == playerStartLocation ||
                   pit2StartLocation == wumpusStartLocation ||
                   TwoObjectsLocateNearby(playerStartLocation, pit2StartLocation))
            {
                pit2StartLocation = new Location(RNG.NumberBetween(0, MAZE_DIMENSION),
                                                 RNG.NumberBetween(0, MAZE_DIMENSION));
            }
            _pit2 = new Pit(pit2StartLocation);
            _pit2Revealed = false;

            GameObject[] gameObjects = new GameObject[]
            {
                _player,
                _wumpus,
                _pit1,
                _pit2
            };

            _maze = new Maze(MAZE_DIMENSION, gameObjects);
        }

        public void Run()
        {
            while (_player.IsAlive && _wumpus.IsAlive)
            {
                _maze.Update();
                Render();
                PrintMessages();
                Messenger.ClearMessages();

                ConsoleKey actionKey = Console.ReadKey(true).Key;
                PerformPlayerAction(actionKey);

                Direction wumpusDirection = (Direction)RNG.NumberBetween(0, 4);
                PerformWumpusMove(wumpusDirection);

                if (TwoObjectsLocateNearby(_player.GetLocation(), _wumpus.GetLocation()))
                {
                    Messenger.AddMessage("?? ?????????? ?????????????? ?????..");
                }
                if (TwoObjectsLocateNearby(_player.GetLocation(), _pit1.GetLocation()) ||
                    TwoObjectsLocateNearby(_player.GetLocation(), _pit2.GetLocation()))
                {
                    Messenger.AddMessage("?? ???????? ????????..");
                }

                if (WumpusAtePlayer())
                {
                    _wumpusRevealed = true;
                    Messenger.AddMessage("?????? ???? ???!");
                    _maze.Update();
                    Render();
                    PrintMessages();
                    _player.IsAlive = false;
                }
                else if (PlayerFellIntoThePit())
                {
                    _pit1Revealed = true;
                    _pit2Revealed = true;
                    Messenger.AddMessage("?? ????? ? ???!");
                    _maze.Update();
                    Render();
                    PrintMessages();
                    _player.IsAlive = false;
                }
            }

            if (!_player.IsAlive)
            {
                Messenger.AddMessage("???? ?????????.");
            }
            else if (!_wumpus.IsAlive)
            {
                Messenger.AddMessage("?? ????? ???????. ??????!");
            }

            _maze.Update();
            Render();
            PrintMessages();
        }

        /// <summary>
        /// Renders maze.
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
                        case CellContent.Pit: 
                            if (_pit1Revealed || _pit2Revealed) // ???????? ??????!!! ?????? ???????????? ?????? ???? ???
                            {
                                Console.Write("[");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("O");
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
        }

        /// <summary>
        /// Prints messages to console.
        /// </summary>
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

            Console.WriteLine("\nX - ?????");
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
            int moveProbability = RNG.NumberBetween(0, 100);
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

        private bool PlayerFellIntoThePit()
        {
            return _player.GetLocation() == _pit1.GetLocation() ||
                   _player.GetLocation() == _pit2.GetLocation();
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
