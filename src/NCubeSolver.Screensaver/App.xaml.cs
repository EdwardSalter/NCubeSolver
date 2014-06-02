using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace NCubeSolver.Screensaver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // Used to host WPF content in preview mode, attach HwndSource to parent Win32 window.
        private HwndSource m_winWpfContent;
        private MainWindow m_winSaver;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mode = e.Args.Any() ? e.Args[0] : "/s";

            // Preview mode--display in little window in Screen Saver dialog
            // (Not invoked with Preview button, which runs Screen Saver in
            // normal /s mode).
            if (mode.ToLower().StartsWith("/p"))
            {
                m_winSaver = new MainWindow();

                Int32 previewHandle = Convert.ToInt32(e.Args[1]);
                //WindowInteropHelper interopWin1 = new WindowInteropHelper(win);
                //interopWin1.Owner = new IntPtr(previewHandle);

                var pPreviewHnd = new IntPtr(previewHandle);

                var lpRect = new RECT();
                Win32API.GetClientRect(pPreviewHnd, ref lpRect);

                var sourceParams = new HwndSourceParameters("sourceParams")
                {
                    PositionX = 0,
                    PositionY = 0,
                    Height = lpRect.Bottom - lpRect.Top,
                    Width = lpRect.Right - lpRect.Left,
                    ParentWindow = pPreviewHnd,
                    WindowStyle = (int)(WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD | WindowStyles.WS_CLIPCHILDREN)
                };

                m_winWpfContent = new HwndSource(sourceParams);
                m_winWpfContent.Disposed += winWPFContent_Disposed;
                m_winWpfContent.RootVisual = m_winSaver.Grid;
                // ReSharper disable once CSharpWarnings::CS4014
                m_winSaver.StartAnimation();
            }

            // Normal screensaver mode.  Either screen saver kicked in normally,
            // or was launched from Preview button
            else if (mode.ToLower().StartsWith("/s"))
            {
                var win = new MainWindow { WindowState = WindowState.Maximized };
                win.Show();
            }

            // Config mode, launched from Settings button in screen saver dialog
            else if (mode.ToLower().StartsWith("/c"))
            {
                var win = new SettingsWindow();
                win.Show();
            }

            // If not running in one of the sanctioned modes, shut down the app
            // immediately (because we don't have a GUI).
            else
            {
                Current.Shutdown();
            }
        }

        /// <summary>
        /// Event that triggers when parent window is disposed--used when doing
        /// screen saver preview, so that we know when to exit.  If we didn't
        /// do this, Task Manager would get a new .scr instance every time
        /// we opened Screen Saver dialog or switched dropdown to this saver.
        /// </summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        void winWPFContent_Disposed(object sender, EventArgs e)
        {
            m_winSaver.Close();
            //            Application.Current.Shutdown();
        }
    }
}
