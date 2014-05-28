﻿using System.Collections.Generic;
using Core;
using NCubeSolver.Core;
using NCubeSolver.Plugins.ConfigurationGenerators;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests
{
    public class ConfigurationGenerator
    {
        internal static CubeConfiguration<FaceColour> GenerateRandomConfiguration(int cubeSize, int randomRotations)
        {
            var generator = new RandomCubeConfigurationGenerator();
            var configuration = generator.GenerateConfiguration(cubeSize, randomRotations);
            return configuration;
        }

        public static IEnumerable<IRotation> GenerateRandomRotations(int numberOfRotations)
        {
            var generator = new RandomCubeConfigurationGenerator();
            var configuration = generator.GenerateRandomRotationList(numberOfRotations);
            return configuration;
        }
    }
}