namespace NCubeSolvers.Core
{
    public abstract class RotationBase : IRotation
    {
        public RotationDirection Direction { get; set; }
        public int Count { get; set; }
        public abstract IRotation Reverse();

        protected RotationBase()
        {
            Count = 1;
            Direction = RotationDirection.Clockwise;
        }

        protected abstract string GetNamePart();

        public string GetName() {
            string letter = GetNamePart();

            if (Count > 1) {
                letter += Count;
            }

            if (Direction == RotationDirection.AntiClockwise)
            {
                letter += "'";
            }


            return letter;
        }

        protected string ReverseName(string name)
        {
            var indexOfReverse = name.IndexOf('\'');
            return indexOfReverse >= 0 ? name.Replace("'", string.Empty) : name + "'";
        }

        public override string ToString()
        {
            return GetName();
        }
    }
}