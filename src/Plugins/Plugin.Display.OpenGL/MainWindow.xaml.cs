﻿namespace NCubeSolver.Plugins.Display.OpenGL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var keyHandler = new KeyHandler(this, DisplayControl.Scene);
            KeyDown += keyHandler.OnKeyDown;
        }
    }
}