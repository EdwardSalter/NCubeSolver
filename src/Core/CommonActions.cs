using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCubeSolvers.Core
{
    public static class CommonActions
    {
        public static async Task<CubeRotation> PositionOnBottom(CubeConfiguration<FaceColour> config, FaceType face)
        {
            CubeRotation cubeRotation;

            switch (face)
            {
                case FaceType.Down:
                    return null;

                case FaceType.Upper:
                    cubeRotation = CubeRotations.X2;
                    break;

                case FaceType.Right:
                    cubeRotation = CubeRotations.ZClockwise;
                    break;

                case FaceType.Left:
                    cubeRotation = CubeRotations.ZAntiClockwise;
                    break;

                case FaceType.Front:
                    cubeRotation = CubeRotations.XAntiClockwise;
                    break;

                case FaceType.Back:
                    cubeRotation = CubeRotations.XClockwise;
                    break;

                default:
                    throw new Exception("Unknown face");
            }

            await config.RotateCube(cubeRotation).ConfigureAwait(false);
            return cubeRotation;
        }

        public static async Task<CubeRotation> PositionOnBottom(CubeConfiguration<FaceColour> config, FaceColour faceColour)
        {
            var face = FindFaceWithCentreColour(config, faceColour);
            return await PositionOnBottom(config, face).ConfigureAwait(false);
        }

        public static async Task<CubeRotation> PositionOnFront(CubeConfiguration<FaceColour> config, FaceColour faceColour)
        {
            var face = FindFaceWithCentreColour(config, faceColour);
            CubeRotation cubeRotation;

            switch (face)
            {
                case FaceType.Front:
                    return null;

                case FaceType.Upper:
                    cubeRotation = CubeRotations.XAntiClockwise;
                    break;

                case FaceType.Right:
                    cubeRotation = CubeRotations.YClockwise;
                    break;

                case FaceType.Left:
                    cubeRotation = CubeRotations.YAntiClockwise;
                    break;

                case FaceType.Down:
                    cubeRotation = CubeRotations.XClockwise;
                    break;

                case FaceType.Back:
                    cubeRotation = CubeRotations.Y2;
                    break;

                default:
                    throw new Exception("Unknown face");
            }

            await config.RotateCube(cubeRotation).ConfigureAwait(false);
            return cubeRotation;
        }

        private static FaceType FindFaceWithCentreColour(CubeConfiguration<FaceColour> config, FaceColour faceColour)
        {
            return config.Faces.First(f => f.Value.Centre == faceColour).Key;
        }

        public static async Task ApplyAndAddRotation(IRotation rotation, ICollection<IRotation> rotations, IRotatable configuration)
        {
            if (rotation == null) return;

            rotations.Add(rotation);

            var cubeRotation = rotation as CubeRotation;
            var faceRotation = rotation as FaceRotation;
            if (cubeRotation != null)
            {
                await configuration.RotateCube(cubeRotation).ConfigureAwait(false);
            }
            if (faceRotation != null)
            {
                await configuration.Rotate(faceRotation).ConfigureAwait(false);
            }
        }

        public static async Task ResetToDefaultPosition(CubeConfiguration<FaceColour> configuration)
        {
            await PositionOnBottom(configuration, FaceColour.White).ConfigureAwait(false);

            await PositionOnFront(configuration, FaceColour.Red).ConfigureAwait(false);

        }

        public static async Task ApplyRotations(IEnumerable<IRotation> rotations, CubeConfiguration<FaceColour> configuration)
        {
            foreach (var rotation in rotations)
            {
                var cubeRotation = rotation as CubeRotation;
                var faceRotation = rotation as FaceRotation;

                if (cubeRotation != null)
                {
                    await configuration.RotateCube(cubeRotation).ConfigureAwait(false);
                }
                if (faceRotation != null)
                {
                    await configuration.Rotate(faceRotation).ConfigureAwait(false);
                }
            }
        }
    }
}
