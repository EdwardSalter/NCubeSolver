using System;

namespace NCubeSolvers.Core
{
    public class SolveFailureException : Exception
    {
        public SolveFailureException(string message) : base(message)
        {
        }
    }
}