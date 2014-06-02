using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolver.Runner
{
    public class PluginLoader
    {
        public CompositionContainer Container { get; private set; }

        // ReSharper disable UnassignedField.Compiler
        [ImportMany(typeof(IDisplay))]
        private IEnumerable<Lazy<IDisplay>> m_displays;

        [ImportMany(typeof(ICubeConfigurationGenerator))]
        private IEnumerable<Lazy<ICubeConfigurationGenerator>> m_configurationGenerators;

        [ImportMany(typeof(ISolver))]
        private IEnumerable<Lazy<ISolver>> m_solvers;

        [ImportMany(typeof(ICelebrator))]
        private IEnumerable<Lazy<ICelebrator>> m_celebrators;
        // ReSharper restore UnassignedField.Compiler

        public IEnumerable<ICubeConfigurationGenerator> ConfigurationGenerators
        {
            get { return m_configurationGenerators.Select(s => s.Value); }
        }

        public IEnumerable<IDisplay> Displays
        {
            get { return m_displays.Select(s => s.Value); }
        }

        public IEnumerable<ISolver> Solvers
        {
            get { return m_solvers.Select(s => s.Value); }
        }

        public IEnumerable<ICelebrator> Celebrators
        {
            get { return m_celebrators.Select(s => s.Value); }
        }

        public void LoadPlugins(IEnumerable<ComposablePartCatalog> catalogs = null)
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));

            if (catalogs != null)
            {
                foreach (var additionalCatalog in catalogs)
                {
                    catalog.Catalogs.Add(additionalCatalog);
                }
            }

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                Container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
