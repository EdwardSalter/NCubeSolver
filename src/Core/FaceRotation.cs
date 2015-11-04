namespace NCubeSolvers.Core
{
    public class FaceRotation : RotationBase
    {
        public FaceType Face { get; set; }
        public int LayerNumberFromFace { get; set; }

        public override IRotation Reverse()
        {
            if (Count == 2)
            {
                return this;
            }

            return new FaceRotation
            {
                Face = Face, 
                LayerNumberFromFace = LayerNumberFromFace,
                Direction = RotationDirectionEx.Reverse(Direction), 
                Count = Count, 
                Name = ReverseName(Name)
            };
        }
    }
}