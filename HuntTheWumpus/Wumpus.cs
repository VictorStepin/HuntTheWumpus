namespace HuntTheWumpus
{
    class Wumpus : GameObject, IMovable, ILivable
    {
        public bool IsAlive { get; set; }

        public Wumpus(Location location) : base(location)
        {
            IsAlive = true;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    location = new Location(location.X, location.Y - 1);
                    break;
                case Direction.Right:
                    location = new Location(location.X + 1, location.Y);
                    break;
                case Direction.Down:
                    location = new Location(location.X, location.Y + 1);
                    break;
                case Direction.Left:
                    location = new Location(location.X - 1, location.Y);
                    break;
            }
        }
    }
}
