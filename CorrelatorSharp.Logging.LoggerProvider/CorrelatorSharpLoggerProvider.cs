using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace CorrelatorSharp.Logging.LoggerProvider
{
    [ProviderAlias("CorrelatorSharp")]
    [ExcludeFromCodeCoverage]
    public class CorrelatorSharpLoggerProvider : Microsoft.Extensions.Logging.ILoggerProvider
    {
        private readonly IScopeProvider _scopeProvider;

        public CorrelatorSharpLoggerProvider(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return new CorrelatorSharpLoggerWrapper(CorrelatorSharp.Logging.LogManager.GetLogger(categoryName), _scopeProvider);
        }

        public void Dispose()
        {

        }

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
