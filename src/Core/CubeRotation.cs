namespace NCubeSolvers.Core
{
    public class CubeRotation : RotationBase
    {
        public Axis Axis { get; set; }


        protected override string GetNamePart()
        {
            return Axis.ToString().ToLower();
        }

        public override IRotation Reverse()
        {
            if (Count == 2)
            {
                return this;
            }

            return new CubeRotation
            {
                Axis = Axis,
                Direction = RotationDirectionEx.Reverse(Direction), 
                Count = Count
            };
        }
    }
}