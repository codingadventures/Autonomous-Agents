namespace Assets.Scripts.Pathfinding
{
    public class Location
    {
        public int X, Y;

        private string _code;

        public Location(int x, int y)
        {
            X = x;
            Y = y;
            _code = string.Format("[{0},{1}]", X, Y);
        }

        public override string ToString()
        {
            return _code;
        }
    }
}
