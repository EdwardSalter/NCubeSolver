using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core;
using NUnit.Framework;

namespace Solvers.IntegrationTests
{
    [TestFixture]
    public class RotationListExTests
    {
        private readonly static Random Random = RandomFactory.CreateRandom();

        [Test]
        public void Condense_CalledOnAListRandomListOfRotations_ConfigurationsAppliedWithAndWithoutCondensedListAreTheSame()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var nonCondensedConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
                var condensedConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
                var rotations = GenerateRandomRotationList(50).ToList();
                var condensedRotations = rotations.Condense();
                //Debug.WriteLine("Rotation list size: {0}\tCondensed list size: {1}", rotations.Count, condensedRotations.Count);

                CommonActions.ApplyRotations(rotations, nonCondensedConfiguration);
                CommonActions.ApplyRotations(condensedRotations, condensedConfiguration);

                AssertConfigurationsAreEqual(condensedConfiguration, nonCondensedConfiguration);
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
            var list1 = new List<IRotation> { Rotations.UpperAntiClockwise, Rotations.UpperClockwise, Rotations.LeftClockwise, Rotations.Upper2 };
            for (int i = 0; i < numberOfRotations; i++)
            {
                var random = Random.Next(list1.Count);
                yield return list1[random];
            }
        }
    }
}
