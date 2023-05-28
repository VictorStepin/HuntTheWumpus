namespace HuntTheWumpus
{
    abstract class MazeObject
    {
        public bool Revealed { get; set; }
        
        protected Location location;

        public MazeObject(Location location)
        {
            Revealed = false;
            this.location = location;
        }

        public Location GetLocation()
        {
            return location;
        }
    }
}
