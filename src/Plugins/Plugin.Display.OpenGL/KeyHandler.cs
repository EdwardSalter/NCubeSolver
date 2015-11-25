using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace NCubeSolver.Plugins.Display.OpenGL
{
    class KeyHandler
    {
        private readonly Scene m_scene;
        private readonly Window m_owner;
        private bool m_fullscreen;

        public KeyHandler(Window owner, Scene scene)
        {
            m_owner = owner;
            m_scene = scene;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            var key = (e.Key == Key.System ? e.SystemKey : e.Key);
            switch (key)
            {
                case Key.Space:
                    m_scene.ToggleAnimation();
                    break;

                case Key.H:
                    m_scene.ToggleAnimationHighlights();
                    break;

                case Key.L:
                    m_scene.SpeedUpAnimation();
                    break;

                case Key.K:
                    m_scene.SlowDownAnimation();
                    break;

                case Key.R:
                    m_scene.CancellationTokenSource.Cancel();
                    break;

                case Key.Enter:
                    if ((e.KeyboardDevice.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                    {
                        ToggleFullscreen();
                    }
                    break;
            }
        }

        private void ToggleFullscreen()
        {
            m_fullscreen = !m_fullscreen;

            if (m_fullscreen)
            {
                m_owner.WindowStyle = WindowStyle.None;
                m_owner.WindowState = WindowState.Maximized;
            }
            else
            {
                m_owner.WindowStyle = WindowStyle.ThreeDBorderWindow;
                m_owner.WindowState = WindowState.Normal;
            }
        }
    }
}
