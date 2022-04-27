using Microsoft.Extensions.Options;
using MikyM.Autofac.Extensions_Net5;

namespace MikyM.Common.Application_Net5.CommandHandlers.Helpers
{
    /// <summary>
    /// Command handler options
    /// </summary>
    public sealed class CommandHandlerConfiguration : IOptions<CommandHandlerConfiguration>
    {

        internal CommandHandlerConfiguration(ApplicationConfiguration config)
        {
            Config = config;
        }

        internal ApplicationConfiguration Config { get; set; }

        /// <summary>
        /// Gets or sets the default lifetime for base generic data services
        /// </summary>
        public Lifetime DefaultLifetime { get; set; } = Lifetime.InstancePerLifetimeScope;

        /// <inheritdoc />
        public CommandHandlerConfiguration Value => this;
    }
}