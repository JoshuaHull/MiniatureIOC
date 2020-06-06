using System;

namespace MiniatureIOC
{
    public static class BuildableMiniIOCHandler
    {
        public static MiniIOCHandler Load(this MiniIOCHandler handler)
        {
            handler.LoadDependencies();

            return handler;
        }

        public static MiniIOCHandler Build(this MiniIOCHandler handler)
        {
            if (handler.AllTypes == null)
                throw new Exception("Cannot build before dependencies have been loaded.");

            handler.BuildServiceProvider();

            return handler;
        }

        public static MiniIOCHandler ResolveFor(this MiniIOCHandler handler, object resolveOn)
        {
            if (handler.ServiceProvider == null)
                throw new Exception("Cannot resolve services without a Service Provider.");

            handler.ResolveServicesFor(resolveOn);

            return handler;
        }
    }
}
