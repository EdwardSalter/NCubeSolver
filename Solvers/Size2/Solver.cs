using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Plugins;

namespace NCubeSolver.Plugins.Solvers.Size2
{
    public class Solver : ISolver
    {
        // TODO: ENUM OF CUBE TYPES, SET WHICH TYPE THIS IS SOLVING
        public string PluginName
        {
            get { return "2x2x2 Beginer Method"; }
        }

        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            return null;
        }
    }
}
