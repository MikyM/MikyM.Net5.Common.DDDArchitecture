using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MikyM.Autofac.Extensions_Net5;

namespace MikyM.Common.Application_Net5
{
    /// <summary>
    /// Registration extension configuration
    /// </summary>
    public sealed class ServiceApplicationConfiguration : IOptions<ServiceApplicationConfiguration>
    {
        internal ServiceApplicationConfiguration(ApplicationConfiguration config)
        {
            Config = config;
        }

        internal ApplicationConfiguration Config { get; set; }
        /// <summary>
        /// Gets or sets the default lifetime for base generic data services
        /// </summary>
        public Lifetime BaseGenericDataServiceLifetime { get; set; } = Lifetime.InstancePerLifetimeScope;
        /// <summary>
        /// Gets or sets the default lifetime for custom data services that implement or derive from base data services
        /// </summary>
        public Lifetime DataServiceLifetime { get; set; } = Lifetime.InstancePerLifetimeScope;

        /// <summary>
        /// Gets data interceptor registration delegates
        /// </summary>
        internal Dictionary<Type, DataInterceptorConfiguration> DataInterceptors { get; private set; } = new();

        internal Action<AttributeRegistrationOptions>? AttributeOptions { get; private set; }

        /// <summary>
        /// Marks an interceptor of a given type to be used for intercepting base data services.
        /// Please note you must also add this interceptor using <see cref="ApplicationConfiguration.AddInterceptor{T}"/>
        /// </summary>
        /// <param name="interceptor">Type of the interceptor</param>
        /// <param name="configuration">Interceptor configuration</param>
        /// <returns>Current instance of the <see cref="ServiceApplicationConfiguration"/></returns>
        public ServiceApplicationConfiguration AddDataServiceInterceptor(Type interceptor, DataInterceptorConfiguration configuration = DataInterceptorConfiguration.CrudAndReadOnly)
        {
            DataInterceptors.TryAdd(interceptor ?? throw new ArgumentNullException(nameof(interceptor)), configuration);
            return this;
        }
        /// <summary>
        /// Marks an interceptor of a given type to be used for intercepting base data services.
        /// Please note you must also add this interceptor using <see cref="ApplicationConfiguration.AddInterceptor{T}"/>
        /// </summary>
        /// <param name="configuration">Interceptor configuration</param>
        /// <returns>Current instance of the <see cref="ServiceApplicationConfiguration"/></returns>
        public ServiceApplicationConfiguration AddDataServiceInterceptor<T>(DataInterceptorConfiguration configuration = DataInterceptorConfiguration.CrudAndReadOnly) where T : notnull
        {
            DataInterceptors.TryAdd(typeof(T), configuration);
            return this;
        }

        /// <summary>
        /// Configures attribute services registration options
        /// </summary>
        /// <returns>Current instance of the <see cref="ServiceApplicationConfiguration"/></returns>
        public ServiceApplicationConfiguration ConfigureAttributeServices(Action<AttributeRegistrationOptions> action)
        {
            AttributeOptions = action;
            return this;
        }

        /// <inheritdoc />
        public ServiceApplicationConfiguration Value => this;
    }

    /// <summary>
    /// Configuration for base data service interceptors
    /// </summary>
    public enum DataInterceptorConfiguration
    {
        /// <summary>
        /// Crud and read-only
        /// </summary>
        CrudAndReadOnly,
        /// <summary>
        /// Crud
        /// </summary>
        Crud,
        /// <summary>
        /// Read-only
        /// </summary>
        ReadOnly
    }
}