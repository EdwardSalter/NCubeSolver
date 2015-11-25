using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size2
{
    public class Solver : SolverBase
    {
        public override string PluginName
        {
            get { return "2x2x2 Beginer Method"; }
        }

        public override IEnumerable<int> ForCubeSizes
        {
            get { return new[] { 2 }; }
        }

        public override async Task<IEnumerable<IRotation>> SolveAsync(CubeConfiguration<FaceColour> configuration, CancellationToken cancel)
        {
            return null;
        }
    }
}
