﻿using System;
using System.Linq;

namespace NCubeSolvers.Core
{
    public static class FaceRules
    {
        public static FaceType FaceAtRelativePositionTo(FaceType face, RelativePosition position)
        {
            if (position == RelativePosition.Same) return face;

            switch (face)
            {
                case FaceType.Front:
                    switch (position)
                    {
                        case RelativePosition.Right:
                            return FaceType.Right;
                        case RelativePosition.Left:
                            return FaceType.Left;
                        case RelativePosition.Opposite:
                            return FaceType.Back;
                    }
                    break;
                case FaceType.Right:
                    switch (position)
                    {
                        case RelativePosition.Right:
                            return FaceType.Back;
                        case RelativePosition.Left:
                            return FaceType.Front;
                        case RelativePosition.Opposite:
                            return FaceType.Left;
                    }
                    break;
                case FaceType.Back:
                    switch (position)
                    {
                        case RelativePosition.Right:
                            return FaceType.Left;
                        case RelativePosition.Left:
                            return FaceType.Right;
                        case RelativePosition.Opposite:
                            return FaceType.Front;
                    }
                    break;
                case FaceType.Left:
                    switch (position)
                    {
                        case RelativePosition.Right:
                            return FaceType.Front;
                        case RelativePosition.Left:
                            return FaceType.Back;
                        case RelativePosition.Opposite:
                            return FaceType.Right;
                    }
                    break;
            }

            throw new Exception("Invalid face type");
        }

        public static FaceType GetFaceOfColour(FaceColour colour, CubeConfiguration<FaceColour> configuration)
        {
            return configuration.Faces.First(kvp => kvp.Value.Centre == colour).Key;
        }

        public static RelativePosition RelativePositionBetweenFaces(FaceType primary, FaceType secondary)
        {
            switch (primary)
            {
                case FaceType.Front:
                    switch (secondary)
                    {
                        case FaceType.Front:
                            return RelativePosition.Same;
                        case FaceType.Left:
                            return RelativePosition.Left;
                        case FaceType.Right:
                            return RelativePosition.Right;
                        case FaceType.Back:
                            return RelativePosition.Opposite;
                    }
                    break;
                case FaceType.Right:
                    switch (secondary)
                    {
                        case FaceType.Front:
                            return RelativePosition.Left;
                        case FaceType.Left:
                            return RelativePosition.Opposite;
                        case FaceType.Right:
                            return RelativePosition.Same;
                        case FaceType.Back:
                            return RelativePosition.Right;
                    }
                    break;
                case FaceType.Back:
                    switch (secondary)
                    {
                        case FaceType.Front:
                            return RelativePosition.Opposite;
                        case FaceType.Left:
                            return RelativePosition.Right;
                        case FaceType.Right:
                            return RelativePosition.Left;
                        case FaceType.Back:
                            return RelativePosition.Same;
                    }
                    break;
                case FaceType.Left:
                    switch (secondary)
                    {
                        case FaceType.Front:
                            return RelativePosition.Right;
                        case FaceType.Left:
                            return RelativePosition.Same;
                        case FaceType.Right:
                            return RelativePosition.Opposite;
                        case FaceType.Back:
                            return RelativePosition.Left;
                    }
                    break;
            }

            throw new NotImplementedException("The relative position between " + primary + " and " + secondary + " has not been implemented yet");
        }

        public static Edge EdgeJoiningFaceToFace(FaceType joiningFace, FaceType mainFace)
        {
            switch (mainFace)
            {
                case FaceType.Upper:
                    switch (joiningFace)
                    {
                        case FaceType.Right:
                            return Edge.Right;
                        case FaceType.Left:
                            return Edge.Left;
                        case FaceType.Front:
                            return Edge.Bottom;
                        case FaceType.Back:
                            return Edge.Top;
                    }
                    break;

                case FaceType.Down:
                    switch (joiningFace)
                    {
                        case FaceType.Right:
                            return Edge.Right;
                        case FaceType.Left:
                            return Edge.Left;
                        case FaceType.Front:
                            return Edge.Top;
                        case FaceType.Back:
                            return Edge.Bottom;
                    }
                    break;

                case FaceType.Left:
                    switch (joiningFace)
                    {
                        case FaceType.Down:
                            return Edge.Bottom;
                        case FaceType.Upper:
                            return Edge.Top;
                        case FaceType.Front:
                            return Edge.Right;
                        case FaceType.Back:
                            return Edge.Left;
                    }
                    break;

                case FaceType.Right:
                    switch (joiningFace)
                    {
                        case FaceType.Down:
                            return Edge.Bottom;
                        case FaceType.Upper:
                            return Edge.Top;
                        case FaceType.Front:
                            return Edge.Left;
                        case FaceType.Back:
                            return Edge.Right;
                    }
                    break;

                case FaceType.Front:
                    switch (joiningFace)
                    {
                        case FaceType.Down:
                            return Edge.Bottom;
                        case FaceType.Upper:
                            return Edge.Top;
                        case FaceType.Left:
                            return Edge.Left;
                        case FaceType.Right:
                            return Edge.Right;
                    }
                    break;

                case FaceType.Back:
                    switch (joiningFace)
                    {
                        case FaceType.Down:
                            return Edge.Bottom;
                        case FaceType.Upper:
                            return Edge.Top;
                        case FaceType.Left:
                            return Edge.Right;
                        case FaceType.Right:
                            return Edge.Left;
                    }
                    break;
            }

            throw new Exception("Invalid face");
        }
    }

    public enum RelativePosition
    {
        Same,
        Left,
        Right,
        Opposite
    }
}
