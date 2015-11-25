using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NCubeSolvers.Core;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolver.Plugins.Solvers
{
    public abstract class SolverBase : ISolver
    {
        private void CheckConfigurationIsValidForThisSolver(CubeConfiguration<FaceColour> configuration)
        {
            if (!ForCubeSizes.Contains(configuration.Size))
                throw new Exception("This solver cannot handle a configuration of this size");
        }

        public abstract string PluginName { get; }

        public bool SkipChecks { get; set; }

        public virtual Task<IEnumerable<IRotation>> SolveAsync(CubeConfiguration<FaceColour> configuration, CancellationToken cancel)
        {
            if (!SkipChecks)
            {
                CheckConfigurationIsValidForThisSolver(configuration);
            }

            return Task.FromResult(new List<IRotation>().AsEnumerable());
        }

        public abstract IEnumerable<int> ForCubeSizes { get; }
    }
}