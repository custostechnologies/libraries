using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Custos.XrmHelpers;

namespace Custos.FunctionHelpers.Logging
{
    public class FunctionLoggerFactory : IDataverseLoggerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FunctionLoggerFactory(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataverseLogger Create<T>()
        {
            return new FunctionLogger(
                _serviceProvider.GetRequiredService<
                    ILogger<FunctionLogger>>(),
                typeof(T).Name
            );
        }
    }
}