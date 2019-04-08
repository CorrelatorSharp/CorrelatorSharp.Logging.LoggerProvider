using System;

namespace CorrelatorSharp.Logging.LoggerProvider
{
    public interface IScopeProvider
    {
        IDisposable BeginScope<TState>(TState state);
        void ErrorBuildingScope(Exception exception);
    }
}