using System;
using System.Collections.Generic;
using System.Text;

namespace MiniatureIOC
{
    public class MiniIOCHandlerFactory
    {
        private static string PreviousAssemblyRegex = null;
        private static MiniIOCHandler PreviousHandler = null;

        public static MiniIOCHandler GetOrCreateHandler(string assemblyRegex, bool forceReload=false)
            => GetOrCreateHandler(assemblyRegex, regex => new MiniIOCHandler(regex), forceReload);

        public static T GetOrCreateHandler<T>(string assemblyRegex, Func<string, T> newInstance, bool forceReload=false)
            where T : MiniIOCHandler
        {
            if (PreviousAssemblyRegex != assemblyRegex || PreviousHandler == null || forceReload) {
                PreviousHandler = CreateHandler(assemblyRegex, newInstance);
                PreviousAssemblyRegex = assemblyRegex;
            }

            return (T)PreviousHandler;
        }

        private static T CreateHandler<T>(string assemblyRegex, Func<string, T> newInstance)
            where T : MiniIOCHandler
            => newInstance(assemblyRegex);
    }
}
