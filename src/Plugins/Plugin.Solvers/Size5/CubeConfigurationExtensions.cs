using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal static class CubeConfigurationExtensions
    {
        public static int InnerLayerIndex<T>(this CubeConfiguration<T> configuration)
        {
            return configuration.GetCentreLayer() - 1;
        }
    }
}
