namespace Core
{
    public static class RotationDirectionEx
    {
        public static RotationDirection Reverse(RotationDirection direction)
        {
            return direction == RotationDirection.Clockwise
                ? RotationDirection.AntiClockwise
                : RotationDirection.Clockwise;
        }
    }
}
