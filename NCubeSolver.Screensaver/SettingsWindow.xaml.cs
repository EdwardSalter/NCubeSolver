using System.Windows;
using NCubeSolver.Screensaver.Properties;

namespace NCubeSolver.Screensaver
{
    /// <summary>
    /// Interaction logic for SettingsWindows.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
            Close();
        }
    }
}
