using System.Collections.Generic;
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

        public IEnumerable<SupportedCubeSize> SupportedCubeSizes { get; private set; }
        public Settings Settings { get; private set; }

        public SettingsWindow(DisplayControl display = null)
        {
            Settings = Settings.Default;
            m_display = display;

            // TODO: USE REFLECTION / MEF TO FIND OUT WHAT IS AVAILABLE
            SupportedCubeSizes = new[]
            {
                new SupportedCubeSize { Name = "Random", CubeSize = null },
                new SupportedCubeSize { Name = "3x3x3", CubeSize = 3 },
                new SupportedCubeSize { Name = "5x5x5", CubeSize = 5 }
            };

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

            Settings.Reload();
            Close();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Save();
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
