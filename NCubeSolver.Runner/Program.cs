using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Core;
using Core.Plugins;
using NCubeSolver.Runner.Properties;

namespace NCubeSolver.Runner
{
    class Program
    {
        private static IDisplay _display;
        private static ISolver _solver;
        private static ICelebrator _celebrator;
        private static readonly PluginLoader PluginLoader = new PluginLoader();
        private static ICubeConfigurationGenerator _configurationGenerator;
        private static bool _running = true;

        private static SolveRun _run;

        [STAThread]
        static void Main()
        {
            LoadPlugins();
            _run = new SolveRun(_configurationGenerator, _solver, _display, _celebrator);


            Console.WriteLine("Initialising display adaptor: {0}", _display.PluginName);
            _display.Closed += OnDisplayClosed;
            _display.Initialise().Wait();


            while (_running)
            {
                _run.Run().Wait();
            }

            Console.ReadLine();
        }

        private static void LoadPlugins()
        {
            var catalogs = new List<ComposablePartCatalog>
            {
                new DirectoryCatalog("Extensions", "*.dll"),
            };
            PluginLoader.LoadPlugins(catalogs);
            SetupPlugins();
        }

        private static void SetupPlugins()
        {
            _display = PluginLoader.Displays.First(d => d.PluginName == Settings.Default.DisplayPluginName);
            _configurationGenerator = PluginLoader.ConfigurationGenerators.First(g => g.PluginName == Settings.Default.GeneratorPluginName);
            _solver = PluginLoader.Solvers.First(s => s.PluginName == Settings.Default.SolverPluginName);
            _celebrator = PluginLoader.Celebrators.First(c => c.PluginName == Settings.Default.CelebratorPluginName);
        }

        private static void OnDisplayClosed(object sender, EventArgs args)
        {
            _running = false;
            Console.WriteLine();
            Console.WriteLine("Display closed");
            //Console.WriteLine("Press enter to exit");
            Environment.Exit(0);
        }
    }
}
