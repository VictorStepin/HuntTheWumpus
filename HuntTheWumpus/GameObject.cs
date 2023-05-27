namespace HuntTheWumpus
{
    abstract class GameObject
    {
        protected Location location;

        public GameObject(Location location)
        {
            this.location = location;
        }

        public Location GetLocation()
        {
            return location;
        }
    }
}
