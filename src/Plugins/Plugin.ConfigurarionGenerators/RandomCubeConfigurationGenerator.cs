using System;
using System.Collections.Generic;
using NCubeSolvers.Core;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolver.Plugins.ConfigurationGenerators
{
    public class RandomCubeConfigurationGenerator : ICubeConfigurationGenerator
    {
        private readonly System.Random m_random = new System.Random(Guid.NewGuid().GetHashCode());

        public CubeConfiguration<FaceColour> GenerateConfiguration(int size, int numberOfRotations)
        {
            return CreateRandomConfiguration(size, numberOfRotations);
        }

        private CubeConfiguration<FaceColour> CreateRandomConfiguration(int size, int moves)
        {
            var cubeConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(size);
            var rotations = GenerateRandomRotationList(moves, size);

            CommonActions.ApplyRotations(rotations, cubeConfiguration);

            return cubeConfiguration;
        }

        public List<IRotation> GenerateRandomRotationList(int moves, int? size = null)
        {
            var rotations = new List<IRotation>();

            for (int i = 0; i < moves; i++)
            {
                if (m_random.Next(0, 2) == 1)
                {
                    rotations.Add(Rotations.Random(size));
                }
                else
                {
                    rotations.Add(CubeRotations.Random());
                }
            }
            return rotations;
        }

        public string PluginName
        {
            get { return "Random"; }
        }
    }
}
