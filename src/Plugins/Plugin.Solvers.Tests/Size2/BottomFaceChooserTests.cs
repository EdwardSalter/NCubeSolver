using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size2;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.UnitTests.Size2
{
    public class BottomFaceChooserTests
    {
        [Test]
        public async Task Method_Action_Expected()
        {
            var solver = new BottomFaceChooser();
            var configuration = Helpers.CreateConfiguration(new FaceRotation[]{}, 2);

            var faceColour = solver.ChooseFaceColourForBottom(configuration);

            // TODO: SOME ASSERT
        } 
    }
}