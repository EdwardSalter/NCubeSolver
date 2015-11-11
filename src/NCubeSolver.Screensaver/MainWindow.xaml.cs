using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NCubeSolver.Plugins.Celebrators;
using NCubeSolver.Plugins.ConfigurationGenerators;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolver.Screensaver.Properties;
using NCubeSolvers.Core;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolver.Screensaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Point? m_lastMouse;
        private bool m_settingsJustShown;

        public MainWindow()
        {
            InitializeComponent();

            DisplayControl.Scene.ShowAxes = false;
            DisplayControl.Scene.AnimationLength = Settings.Default.AnimationLength;
            DisplayControl.ShowFPS = false;
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_settingsJustShown)
            {
                m_settingsJustShown = false;
                return;
            }

            var currentPos = e.GetPosition(this);
            if (m_lastMouse != null &&
                (Math.Abs(currentPos.X - m_lastMouse.Value.X) > 1 || Math.Abs(currentPos.Y - m_lastMouse.Value.Y) > 1)
                )
                Close();
            m_lastMouse = currentPos;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                new SettingsWindow(DisplayControl).ShowDialog();
                m_settingsJustShown = true;
            }
            else
                Close();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // ReSharper disable once CSharpWarnings::CS4014
            StartAnimation();
        }

        public async Task StartAnimation()
        {
            var configurationGenerator = new RandomCubeConfigurationGenerator();
            var solver3x3x3 = new BeginerMethod();
            var solver5x5x5 = new SimpleSolver();

            var celebrator = new TimeDelayCelebrator(2000);

            // TODO: LOAD ALL SOLVERS + GENERATORS, GENERATE A CONFIG WITH RANDOM SIZE, PICK A SOLVER BASED ON THE CONFIGURATION GIVEN

            
            var random = new Random();

            while (true)
            {
                int cubeSize = 3;
                ISolver solver = solver3x3x3;
                if (random.NextDouble() < 0.25)
                {
                    cubeSize = 5;
                    solver = solver5x5x5;
                }

                var run = new SolveRun(configurationGenerator, solver, DisplayControl, celebrator, cubeSize);
                await run.Run();
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
