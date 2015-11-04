namespace NCubeSolvers.Core
{
    public class CubeRotation : RotationBase
    {
        public Axis Axis { get; set; }
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
                Count = Count, 
                Name = ReverseName(Name)
            };
        }
    }
}