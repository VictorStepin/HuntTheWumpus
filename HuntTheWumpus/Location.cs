namespace HuntTheWumpus
{
    struct Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Location (int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator == (Location l1, Location l2)
        {
            return l1.X == l2.X && l1.Y == l2.Y;
        }

        public static bool operator != (Location l1, Location l2)
        {
            return l1.X != l2.X || l1.Y != l2.Y;
        }
    }
}
