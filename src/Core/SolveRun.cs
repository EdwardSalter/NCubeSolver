using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolvers.Core
{
    public class SolveRun
    {
        private readonly ICubeConfigurationGenerator m_generator;
        private readonly ISolver m_solver;
        private readonly IDisplay m_display;
        private readonly ICelebrator m_celebrator;
        private CubeConfiguration<FaceColour> m_configuration;
        private int m_currentStep;
        private readonly int m_cubeSize;
        private CancellationToken m_cancellationToken;

        public SolveRun(ICubeConfigurationGenerator generator, ISolver solver, IDisplay display, ICelebrator celebrator, int cubeSize)
        {
            m_display = display;
            m_celebrator = celebrator;
            m_solver = solver;
            m_generator = generator;
            m_cubeSize = cubeSize;
        }

        public async Task Run()
        {
            // TODO: USE NLOG
            Console.WriteLine("Creating cube configuration");

            m_cancellationToken = new CancellationToken(false);

            var numRotations = (int)(Math.Pow(m_cubeSize, 3) * 2);
            m_configuration = m_generator.GenerateConfiguration(m_cubeSize, numRotations);
            if (m_display != null)
                await m_display.SetCubeConfiguration(m_configuration).ConfigureAwait(true);


            // TODO: PAUSES?
            Console.WriteLine("Solving");
            try
            {
                var solution = (await m_solver.SolveAsync(m_configuration, m_cancellationToken).ConfigureAwait(true)).ToList();

                Console.WriteLine("Solution ({0} steps): {1}", solution.Count, string.Join(" ", solution));

                m_currentStep = 0;
                foreach (var step in solution)
                {
                    await RunStep(step, solution.Count).ConfigureAwait(true);

                }

                await m_celebrator.Celebrate().ConfigureAwait(true);

            }
            catch (SolveFailureException)
            {
                Console.WriteLine("Failed to find a solution.");
            }
        }

        private async Task RunStep(IRotation rotation, int solutionCount)
        {
            Console.WriteLine("Executing step {0}/{1}: {2}", ++m_currentStep, solutionCount, rotation);

            var cubeRotation = rotation as CubeRotation;
            var faceRotation = rotation as FaceRotation;
            var tasks = new List<Task>();

            if (cubeRotation != null)
            {
                tasks.Add(m_configuration.RotateCube(cubeRotation));
                if (m_display != null)
                    tasks.Add(m_display.RotateCube(cubeRotation));
            }
            if (faceRotation != null)
            {
                tasks.Add(m_configuration.Rotate(faceRotation));

                if (m_display != null)
                    tasks.Add(m_display.Rotate(faceRotation));
            }

            await Task.WhenAll(tasks).ConfigureAwait(true);

        }
    }
}
