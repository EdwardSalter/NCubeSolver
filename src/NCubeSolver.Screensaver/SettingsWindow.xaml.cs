using System.Windows;
using NCubeSolver.Plugins.Display.OpenGL;
using NCubeSolver.Screensaver.Properties;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolver.Screensaver
{
    /// <summary>
    /// Interaction logic for SettingsWindows.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        private readonly DisplayControl m_display;
        private readonly int m_oldSpeed;

        public SettingsWindow(DisplayControl display = null)
        {
            m_display = display;

            if (m_display != null)
            {
                m_oldSpeed = m_display.Scene.AnimationLength;
            }

            InitializeComponent();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            if (m_display != null)
            {
                m_display.Scene.AnimationLength = m_oldSpeed;
            }

            Settings.Default.Reload();
            Close();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
            Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_display != null)
            {
                m_display.Scene.AnimationLength = (int)e.NewValue;
            }
        }
    }
}
