using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size2
{
    public class Solver : SolverBase
    {
        private readonly BottomFaceChooser m_bottomFaceChooser = new BottomFaceChooser();
        private readonly BottomLayerSolver m_bottomFaceSolver = new BottomLayerSolver();

        public override string PluginName
        {
            get { return "2x2x2 Beginer Method"; }
        }

        public override IEnumerable<int> ForCubeSizes
        {
            get { return new[] { 2 }; }
        }

        public override async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var bottomFaceAndColour = m_bottomFaceChooser.ChooseFaceColourForBottom(configuration);
            var moveToBottom = await CommonActions.PositionOnBottom(configuration, bottomFaceAndColour.Face);
            if (moveToBottom != null)
            {
                solution.Add(moveToBottom);
            }

            var bottomColour = bottomFaceAndColour.Colour;

            var stepsToSolveBottomFace = await m_bottomFaceSolver.Solve(bottomColour, configuration);
            solution.AddRange(stepsToSolveBottomFace);

            return solution;
        }
    }
}
