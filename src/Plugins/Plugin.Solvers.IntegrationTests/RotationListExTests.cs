using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NCubeSolver.Core;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests
{
    [TestFixture]
    public class RotationListExTests
    {
        private static readonly Random Random = RandomFactory.CreateRandom();

        [Test]
        public void Condense_CalledOnAListRandomListOfRotations_ConfigurationsAppliedWithAndWithoutCondensedListAreTheSame()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var nonCondensedConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
                var condensedConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
                var rotations = GenerateRandomRotationList(50).ToList();
                var condensedRotations = rotations.Condense().ToList();
                //Debug.WriteLine("Rotation list size: {0}\tCondensed list size: {1}", rotations.Count, condensedRotations.Count);

                Console.WriteLine("NonCondensedRoations: {0}", string.Join(" ", rotations.Select(f => f.GetName())));
                Console.WriteLine("CondensedRoations: {0}", string.Join(" ", condensedRotations.Select(f => f.GetName())));

                CommonActions.ApplyRotations(rotations, nonCondensedConfiguration);
                CommonActions.ApplyRotations(condensedRotations, condensedConfiguration);

                AssertConfigurationsAreEqual(nonCondensedConfiguration, condensedConfiguration);
            });
        }

        private static void AssertConfigurationsAreEqual(CubeConfiguration<FaceColour> configuration1, CubeConfiguration<FaceColour> configuration2)
        {
            CollectionAssert.AreEqual(configuration1.Faces[FaceType.Front].Items, configuration2.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(configuration1.Faces[FaceType.Back].Items, configuration2.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(configuration1.Faces[FaceType.Left].Items, configuration2.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(configuration1.Faces[FaceType.Right].Items, configuration2.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(configuration1.Faces[FaceType.Upper].Items, configuration2.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(configuration1.Faces[FaceType.Down].Items, configuration2.Faces[FaceType.Down].Items);
        }

        private static IEnumerable<IRotation> GenerateRandomRotationList(int numberOfRotations)
        {
            IRotation previousRotation = Rotations.Random();
            for (int i = 0; i < numberOfRotations; i++)
            {
                IRotation rotation;

                var type = Random.NextDouble();
                if (type < 0.333)
                {
                    var cubeOrFace = Random.NextDouble();
                    if (cubeOrFace < 0.5)
                    {
                        rotation = Rotations.Random();
                    }
                    else
                    {
                        rotation = CubeRotations.Random();
                    }
                }
                else if (type < 0.666)
                {
                    rotation = previousRotation.Reverse();
                }
                else
                {
                    rotation = previousRotation;
                }

                previousRotation = rotation;
                yield return rotation;
            }
        }
    }
}
