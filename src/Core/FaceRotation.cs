using System;
using System.Linq;

namespace NCubeSolvers.Core
{
    public class FaceRotation : RotationBase
    {
        public FaceType Face { get; set; }
        public int LayerNumberFromFace { get; set; }

        protected override string GetNamePart()
        {
            string letter;
            switch (Face)
            {
                case FaceType.Upper:
                    letter = "U"; break;
                case FaceType.Down:
                    letter = "D"; break;
                case FaceType.Front:
                    letter = "F"; break;
                case FaceType.Back:
                    letter = "B"; break;
                case FaceType.Left:
                    letter = "L"; break;
                case FaceType.Right:
                    letter = "R"; break;
                default:
                    throw new Exception("Unknown Face");
            }

            
                var superscriptPart = GetUnicodePart(LayerNumberFromFace);
                letter += superscriptPart;
            

            return letter;
        }

        private string GetUnicodePart(int number) {
            if (number == 0)
            {
                return string.Empty;
            }

            var digits = (number + 1).ToString().Select(digit => digit.ToString());
            var codes = digits.Select(digit => int.Parse("208" + digit, System.Globalization.NumberStyles.HexNumber));

            var unicode = string.Join(string.Empty, codes.Select(code => Char.ConvertFromUtf32(code)));
            return unicode;
        }

        public override IRotation Reverse()
        {
            if (Count == 2)
            {
                return this;
            }

            return new FaceRotation
            {
                Face = Face,
                LayerNumberFromFace = LayerNumberFromFace,
                Direction = RotationDirectionEx.Reverse(Direction),
                Count = Count
            };
        }
    }
}