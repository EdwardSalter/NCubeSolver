using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests
{
    [TestFixture]
    public class FaceRotationTests
    {
        [TestCase(FaceType.Upper, "U")]
        [TestCase(FaceType.Down, "D")]
        [TestCase(FaceType.Left, "L")]
        [TestCase(FaceType.Right, "R")]
        [TestCase(FaceType.Front, "F")]
        [TestCase(FaceType.Back, "B")]
        public void GetName_RotationIsOnFace_NameStartsWithCorrectLetter(FaceType face, string firstLetter)
        {
            var rotation = new FaceRotation { Face = face };

            var name = rotation.GetName();

            StringAssert.StartsWith(firstLetter, name);
        }

        [Test]
        public void GetName_RotationIsClockwise_NameDoesNotEndWithApostrophe()
        {
            var rotation = new FaceRotation { Direction = RotationDirection.Clockwise };

            var name = rotation.GetName();

            StringAssert.DoesNotEndWith("'", name);
        }

        [Test]
        public void GetName_RotationIsAntiClockwise_NameEndsWithApostrophe()
        {
            var rotation = new FaceRotation { Direction = RotationDirection.AntiClockwise };

            var name = rotation.GetName();

            StringAssert.EndsWith("'", name);
        }

        [Test]
        public void GetName_RotationHasACounOf1_NameDoesNotEndWithTheCount()
        {
            var rotation = new FaceRotation { Count = 1 };

            var name = rotation.GetName();

            StringAssert.DoesNotEndWith("1", name);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GetName_RotationHasACountGreaterThan1_NameEndsWithTheCount(int count)
        {
            var rotation = new FaceRotation { Count = count };

            var name = rotation.GetName();

            StringAssert.EndsWith(count.ToString(), name);
        }

        [Test]
        public void GetName_RotationIsForLayer0_NameDoesNotContainAUnicodeCharacter()
        {
            var rotation = new FaceRotation { LayerNumberFromFace = 0 };

            var name = rotation.GetName();

            StringAssert.DoesNotContain("\x2080", name);
            StringAssert.DoesNotContain("\x2081", name);
            StringAssert.DoesNotContain("\x2082", name);
            StringAssert.DoesNotContain("\x2083", name);
            StringAssert.DoesNotContain("\x2084", name); 
            StringAssert.DoesNotContain("\x2085", name);
            StringAssert.DoesNotContain("\x2086", name);
            StringAssert.DoesNotContain("\x2087", name);
            StringAssert.DoesNotContain("\x2088", name);
            StringAssert.DoesNotContain("\x2089", name);
        }

        [TestCase(1, "\x2082")]
        [TestCase(2, "\x2083")]
        [TestCase(3, "\x2084")]
        [TestCase(4, "\x2085")]
        [TestCase(5, "\x2086")]
        [TestCase(6, "\x2087")]
        [TestCase(7, "\x2088")]
        [TestCase(8, "\x2089")]
        public void GetName_RotationIsForLayerNonZeroLayerLessThan9_NameContainsUnicodeCharacterForOneMoreThanTheLayer(int layer, string unicodeChar)
        {
            var rotation = new FaceRotation { LayerNumberFromFace = layer };

            var name = rotation.GetName();

            StringAssert.Contains(unicodeChar, name);
        }

        [TestCase(9, "\x2081\x2080")]
        [TestCase(10, "\x2081\x2081")]
        [TestCase(11, "\x2081\x2082")]
        [TestCase(12, "\x2081\x2083")]
        [TestCase(20, "\x2082\x2081")]
        [TestCase(100, "\x2081\x2080\x2081")]
        public void GetName_RotationIsForLayerLayerNumberGreaterThan8_NameContainsMultipleUnicodeCharacters(int layer, string unicodeChar)
        {
            var rotation = new FaceRotation { LayerNumberFromFace = layer };

            var name = rotation.GetName();

            StringAssert.Contains(unicodeChar, name);
        }
    }
}
