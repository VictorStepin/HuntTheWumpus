namespace HuntTheWumpus
{
    abstract class Movable
    {
        public Location Location { get; private set; }

        public Movable(Location location)
        {
            Location = location;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Location = new Location(Location.X, Location.Y - 1);
                    break;
                case Direction.Right:
                    Location = new Location(Location.X + 1, Location.Y);
                    break;
                case Direction.Down:
                    Location = new Location(Location.X, Location.Y + 1);
                    break;
                case Direction.Left:
                    Location = new Location(Location.X - 1, Location.Y);
                    break;
            }
        }
    }
}
