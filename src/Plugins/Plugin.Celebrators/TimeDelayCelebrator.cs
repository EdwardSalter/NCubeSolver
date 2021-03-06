﻿using System.Threading.Tasks;
using NCubeSolvers.Core.Plugins;

namespace NCubeSolver.Plugins.Celebrators
{
    public class TimeDelayCelebrator : ICelebrator
    {
        private readonly int m_millisecondsDelay;

        public TimeDelayCelebrator()
            : this(2000)
        {
        }

        public TimeDelayCelebrator(int milliseconds)
        {
            m_millisecondsDelay = milliseconds;
        }

        public string PluginName { get { return GetType().Name; } }
        public Task Celebrate()
        {
            return Task.Delay(m_millisecondsDelay);
        }
    }
}
