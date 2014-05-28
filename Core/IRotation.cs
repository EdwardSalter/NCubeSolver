namespace NCubeSolvers.Core
{
    public interface IRotation
    {
        string Name { get; set; }
        RotationDirection Direction { get; set; }
        int Count { get; set; }
        IRotation Reverse();
    }
}
