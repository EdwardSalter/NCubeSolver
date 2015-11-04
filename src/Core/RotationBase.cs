namespace NCubeSolvers.Core
{
    public abstract class RotationBase : IRotation
    {
        public string Name { get; set; }
        public RotationDirection Direction { get; set; }
        public int Count { get; set; }
        public abstract IRotation Reverse();

        protected RotationBase()
        {
            Count = 1;
            Direction = RotationDirection.Clockwise;
        }

        protected string ReverseName(string name)
        {
            var indexOfReverse = name.IndexOf('\'');
            return indexOfReverse >= 0 ? name.Replace("'", string.Empty) : name + "'";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}