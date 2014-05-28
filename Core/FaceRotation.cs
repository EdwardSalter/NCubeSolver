namespace Core
{
    public class FaceRotation : RotationBase
    {
        public FaceType Face { get; set; }

        public override IRotation Reverse()
        {
            return new FaceRotation { Face = Face, Direction = RotationDirectionEx.Reverse(Direction), Count = Count, Name = ReverseName(Name) };
        }
    }
}