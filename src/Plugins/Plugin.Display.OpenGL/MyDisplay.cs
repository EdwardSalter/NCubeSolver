using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using NCubeSolvers.Core;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolver.Plugins.Display.OpenGL
{
    class MyDisplay : IDisplay
    {
        public event EventHandler Closed;

        public string PluginName
        {
            get { return "OpenGLWithWindow"; }
        }

        private readonly TaskCompletionSource<bool> m_completionSource = new TaskCompletionSource<bool>();
        private MainWindow m_window;

        public async Task Initialise()
        {
            // Create a thread
            var newWindowThread = new Thread(() =>
            {
                // Create our context, and install it:
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(
                        Dispatcher.CurrentDispatcher));

                m_window = new MainWindow();

                // When the window closes, shut down the dispatcher
                m_window.Closed += OnWindowClosed;

                m_window.Show();
                m_window.Activate();

                m_window.DisplayControl.Ready += (sender, args) => m_completionSource.SetResult(true);
                // Start the Dispatcher Processing
                Dispatcher.Run();
            });

            newWindowThread.SetApartmentState(ApartmentState.STA);
            // Make the thread a background thread
            newWindowThread.IsBackground = true;
            // Start the thread
            newWindowThread.Start();

            await m_completionSource.Task.ConfigureAwait(true);
        }

        private void OnWindowClosed(object s, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.InvokeShutdown();

            if (Closed != null)
            {
                Closed.Invoke(s, e);
            }
        }

        public Task SetCubeConfiguration(CubeConfiguration<FaceColour> configuration)
        {
            return m_window.DisplayControl.SetCubeConfiguration(configuration);
        }

        public void SetCancellation(CancellationTokenSource cancellationToken)
        {
            m_window.DisplayControl.SetCancellation(cancellationToken);
        }

        public void WriteText(string text)
        {
            m_window.DisplayControl.WriteText(text);
        }

        public Task Rotate(FaceRotation faceRotation)
        {
            return m_window.DisplayControl.Rotate(faceRotation);
        }

        public Task RotateCube(CubeRotation rotation)
        {
            return m_window.DisplayControl.RotateCube(rotation);
        }
    }
}
