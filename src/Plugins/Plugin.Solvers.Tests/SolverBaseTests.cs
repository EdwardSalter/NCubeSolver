﻿using System;
using System.Collections.Generic;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.UnitTests
{
    [TestFixture]
    public class SolverBaseTests
    {
        private const int ValidSize = 2;

        private class TestableSolver : SolverBase
        {

            public override string PluginName
            {
                get { throw new NotImplementedException(); }
            }

            public override IEnumerable<int> ForCubeSizes
            {
                get { return new[] { ValidSize }; }
            }
        }

        [Test]
        public void Solve_GivenAConfigurationThatThisSolverCanSolve_DoesNotThrow()
        {
            var solver = new TestableSolver();
            var config = CreateConfigOfSize(ValidSize);

            Assert.DoesNotThrow(() => solver.Solve(config));
        }

        [Test]
        public void Solve_GivenAConfigurationThatThisSolverCannotSolve_Throws()
        {
            var solver = new TestableSolver();
            var config = CreateConfigOfSize(3);

            Assert.Throws<Exception>(() => solver.Solve(config));
        }

        private static CubeConfiguration<FaceColour> CreateConfigOfSize(int size)
        {
            return new CubeConfiguration<FaceColour>(size);
        }
    }
}