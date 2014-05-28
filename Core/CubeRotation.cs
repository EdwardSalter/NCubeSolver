namespace Core
{
    public class CubeRotation : RotationBase
    {
        public Axis Axis { get; set; }
        public override IRotation Reverse()
        {
            return new CubeRotation { Axis = Axis, Direction = RotationDirectionEx.Reverse(Direction), Count = Count, Name = ReverseName(Name) };
        }
    }
}