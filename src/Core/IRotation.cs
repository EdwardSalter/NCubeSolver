namespace NCubeSolvers.Core
{
    public interface IRotation
    {
        string GetName();
        RotationDirection Direction { get; set; }
        int Count { get; set; }
        IRotation Reverse();
    }
}
