namespace HuntTheWumpus
{
    class Player
    {
        public int Location { get; set; }

        public Player()
        {
            Location = 0;
        }

        public void Move (Direction direction)
        {
            if (direction == Direction.Left)
            {
                Location -= 1;
            }
            else
            {
                Location += 1;
            }
        }
    }
}
