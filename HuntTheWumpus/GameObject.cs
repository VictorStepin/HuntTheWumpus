namespace HuntTheWumpus
{
    abstract class GameObject
    {
        protected Location _location;

        public GameObject(Location location)
        {
            _location = location;
        }

        public Location GetLocation()
        {
            return _location;
        }
    }
}
