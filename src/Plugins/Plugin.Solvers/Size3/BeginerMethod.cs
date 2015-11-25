using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    public class BeginerMethod : SolverBase
    {
        private CubeConfiguration<FaceColour> m_configuration;
        private readonly BottomCrossSolver m_bottomCrossSolver = new BottomCrossSolver();
        private readonly BottomLayerSolver m_bottomLayerSolver = new BottomLayerSolver();
        private readonly MiddleLayerSolver m_middleLayerSolver = new MiddleLayerSolver();
        private readonly TopCrossSolver m_topCrossSolver = new TopCrossSolver();
        private readonly TopFaceSolver m_topFaceSolver = new TopFaceSolver();
        private readonly TopLayerSolver m_topLayerSolver = new TopLayerSolver();

        public override IEnumerable<int> ForCubeSizes
        {
            get { return new[] { 3 }; }
        }

        // TODO: MAYBE RETURN THINGS IN SECTIONS?
        public override async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            await base.Solve(configuration).ConfigureAwait(false);

            m_configuration = configuration;

            var solution = new List<IRotation>();

            var cubeRotation = await CommonActions.PositionOnBottom(m_configuration, FaceColour.White).ConfigureAwait(false);

            if (cubeRotation != null)
                solution.Add(cubeRotation);

            var stepsToSolveBottomCross = await m_bottomCrossSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveBottomCross);

            var stepsToSolveBottomLayer = await m_bottomLayerSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveBottomLayer);

            var stepsToSolveMiddleLayer = await m_middleLayerSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveMiddleLayer);

            var stepsToSolveTopCross = await m_topCrossSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveTopCross);

            var stepsToSolveTopFace = await m_topFaceSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveTopFace);

            var stepsToSolveTopLayer = await m_topLayerSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveTopLayer);

            return solution.Condense();
        }

        public override string PluginName
        {
            get { return "BeginerMethod"; }
        }
    }
}
