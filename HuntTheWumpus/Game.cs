using System;

namespace HuntTheWumpus
{
    class Game
    {
        private Labyrinth _labyrinth;
        private Player _player;

        public Game ()
        {
            _labyrinth = new Labyrinth();
            _player = new Player();
        }

        public void Run ()
        {
            bool isRunning = true;
            while (isRunning)
            {
                UpdateCells();

                ConsoleKeyInfo info = Console.ReadKey();

                switch (info.Key)
                {
                    case ConsoleKey.A :
                        if (_player.Location != 0)
                        {
                            _player.Move(Direction.Left);
                        }
                        break;
                    case ConsoleKey.D :
                        if (_player.Location != _labyrinth.Cells.Length - 1)
                        {
                            _player.Move(Direction.Right);
                        }
                        break;
                    case ConsoleKey.X :
                        Console.Clear();
                        isRunning = false;
                        break;
                }
            }
        }

        private void UpdateCells ()
        {
            Console.Clear();
            
            _labyrinth.ClearCells();
            _labyrinth.Cells[_player.Location].Content = CellContent.Player;

            foreach (Cell cell in _labyrinth.Cells)
            {
                switch (cell.Content)
                {
                    case CellContent.Empty :
                        Console.Write("[ ]");
                        break;
                    case CellContent.Player :
                        Console.Write("[@]");
                        break;
                }
            }
            Console.WriteLine();
        }
    }
}