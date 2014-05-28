﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Core.Plugins
{
    [InheritedExport]
    public interface ISolver : IPlugin
    {
        Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration);
    }
}
