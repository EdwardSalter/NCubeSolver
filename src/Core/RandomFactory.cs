﻿using System;

namespace NCubeSolvers.Core
{
    public class RandomFactory
    {
        public static Random CreateRandom()
        {
            Guid guid = Guid.NewGuid();
            int seed = guid.GetHashCode();
            return new Random(seed);
        }
    }
}
