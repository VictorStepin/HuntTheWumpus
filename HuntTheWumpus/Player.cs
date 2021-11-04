namespace HuntTheWumpus
{
    class Player : GameObject, IMovable, ILivable
    {
        public bool IsAlive { get; set; }
        
        public Player(Location location) : base(location)
        {
            IsAlive = true;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    _location = new Location(_location.X, _location.Y - 1);
                    break;
                case Direction.Right:
                    _location = new Location(_location.X + 1, _location.Y);
                    break;
                case Direction.Down:
                    _location = new Location(_location.X, _location.Y + 1);
                    break;
                case Direction.Left:
                    _location = new Location(_location.X - 1, _location.Y);
                    break;
            }
        }
    }
}
