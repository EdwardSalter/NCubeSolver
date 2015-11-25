using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace NCubeSolver.Plugins.Display.OpenGL
{
    class KeyHandler
    {

        private readonly Window m_owner;
        private bool m_fullscreen;
        private readonly DisplayControl m_display;

        public KeyHandler(Window owner, DisplayControl display)
        {
            m_display = display;
            m_owner = owner;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            var key = (e.Key == Key.System ? e.SystemKey : e.Key);
            switch (key)
            {
                case Key.Space:
                    m_display.Scene.ToggleAnimation();
                    break;

                case Key.H:
                    m_display.Scene.ToggleAnimationHighlights();
                    break;

                case Key.L:
                    m_display.Scene.SpeedUpAnimation();
                    break;

                case Key.K:
                    m_display.Scene.SlowDownAnimation();
                    break;

                case Key.R:
                    m_display.Scene.CancellationTokenSource.Cancel();
                    break;

                case Key.T:
                    m_display.ToggleConsoleText();
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
