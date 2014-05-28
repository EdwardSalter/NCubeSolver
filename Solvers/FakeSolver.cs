using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Plugins;

namespace NCubeSolver.Plugins.Solvers
{
    public class FakeSolver : ISolver
    {
        private FaceRotation m_lastFaceRotation;
        private CubeRotation m_lastCubeRotation;
        public string PluginName { get { return "FakeSolver"; } }
        private readonly Random m_random = RandomFactory.CreateRandom();

        public Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
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

            return Task.FromResult<IEnumerable<IRotation>>(solution);
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
