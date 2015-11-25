using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers
{
    public class FakeSolver : SolverBase
    {
        private FaceRotation m_lastFaceRotation;
        private CubeRotation m_lastCubeRotation;
        public override string PluginName { get { return "FakeSolver"; } }
        private readonly Random m_random = RandomFactory.CreateRandom();

        public override IEnumerable<int> ForCubeSizes
        {
            get { return new[] { 2, 3 }; }
        }

        public override async Task<IEnumerable<IRotation>> SolveAsync(CubeConfiguration<FaceColour> configuration, CancellationToken cancel)
        {
            await base.SolveAsync(configuration, cancel).ConfigureAwait(false);
            var solution = new List<IRotation>();

            for (int i = 0; i < 40; i++)
            {
                if (m_random.Next(1, 5) == 1)
                {
                    m_lastCubeRotation = GenerateRandomCubeRotation();
                    solution.Add(m_lastCubeRotation);
                }
                else
                {
                    m_lastFaceRotation = GenerateRandomRotation();
                    solution.Add(m_lastFaceRotation);
                }
            }

            return solution;
        }

        private FaceRotation GenerateRandomRotation()
        {
            FaceRotation faceRotation = Rotations.Random();
            while (m_lastFaceRotation != null && faceRotation.Face == m_lastFaceRotation.Face)
            {
                faceRotation = Rotations.Random();
            }
            return faceRotation;
        }

        private CubeRotation GenerateRandomCubeRotation()
        {
            CubeRotation rotation = CubeRotations.Random();
            while (m_lastCubeRotation != null && rotation.Axis == m_lastCubeRotation.Axis)
            {
                rotation = CubeRotations.Random();
            }
            return rotation;
        }
    }
}
