using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal static class CubeConfigurationExtensions
    {
        public static int MinInnerLayerIndex<T>(this CubeConfiguration<T> configuration)
        {
            return configuration.GetCentreLayer() - 1;
        }

        public static int MaxInnerLayerIndex<T>(this CubeConfiguration<T> configuration)
        {
            return configuration.GetCentreLayer() + 1;
        }

        public static int MinOuterLayerIndex<T>(this CubeConfiguration<T> configuration)
        {
            return configuration.GetCentreLayer() - 2;
        }

        public static int MaxOuterLayerIndex<T>(this CubeConfiguration<T> configuration)
        {
            return configuration.GetCentreLayer() + 2;
        }
    }
}
