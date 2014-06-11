using System;
using System.Linq;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size2
{
    class BottomFaceChooser
    {
        // TODO: TEST
        internal FaceColourPair ChooseFaceColourForBottom(CubeConfiguration<FaceColour> configuration)
        {
            var maxAmountOfColoursSoFar = 0;
            FaceType? chosenFace = null;
            FaceColour? chosenColour = null;

            foreach (var kvp in configuration.Faces)
            {
                var groups = kvp.Value.Items.AsEnumerable().GroupBy(f => f).Select(group => new
                {
                    Colour = group.Key,
                    Count = group.Count()
                }).ToList();

                var maxColoursOnFace = groups.Max(g => g.Count);
                if (maxColoursOnFace > maxAmountOfColoursSoFar)
                {
                    maxAmountOfColoursSoFar = maxColoursOnFace;
                    chosenColour = groups.First(g => g.Count == maxColoursOnFace).Colour;
                    chosenFace = kvp.Key;
                }
            }
            // TODO: DETECT HOW MANY IN CORRECT POS TO EACH OTHER

            if (chosenColour == null) throw new Exception("Did not choose a colour");
            if (chosenFace == null) throw new Exception("Did not choose a face");

            return new FaceColourPair { Face = chosenFace.Value, Colour = chosenColour.Value };
        }
    }
}
