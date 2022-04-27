﻿using System;
using Autofac;
using MikyM.Common.Application_Net5.CommandHandlers.Commands;

namespace MikyM.Common.Application_Net5.CommandHandlers
{
    /// <summary>
    /// Command handler factory
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Gets a <see cref="ICommandHandler"/> of a given type
        /// </summary>
        /// <typeparam name="TCommandHandler">Type of the <see cref="ICommandHandler"/> to get</typeparam>
        /// <returns>Wanted <see cref="ICommandHandler"/></returns>
        TCommandHandler GetHandler<TCommandHandler>() where TCommandHandler : class, ICommandHandler;

        /// <summary>
        /// Gets a <see cref="ICommandHandler"/> for a given <see cref="ICommand{TResult}"/>
        /// </summary>
        /// <typeparam name="TCommand">Type of the <see cref="ICommand{TResult}"/></typeparam>
        /// <typeparam name="TResult">Type of the command result</typeparam>
        /// <returns>Wanted <see cref="ICommandHandler"/></returns>
        ICommandHandler<TCommand, TResult> GetHandlerFor<TCommand, TResult>() where TCommand : class, ICommand<TResult>;
        /// <summary>
        /// Gets a <see cref="ICommandHandler"/> for a given <see cref="ICommand"/>
        /// </summary>
        /// <typeparam name="TCommand">Type of the <see cref="ICommand"/></typeparam>
        /// <returns>Wanted <see cref="ICommandHandler"/></returns>
        ICommandHandler<TCommand> GetHandlerFor<TCommand>() where TCommand : class, ICommand;
    }

    /// <inheritdoc cref="ICommandHandlerFactory"/>
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// Creates a new instance of <see cref="CommandHandlerFactory"/>
        /// </summary>
        /// <param name="lifetimeScope">Autofac's <see cref="ILifetimeScope"/></param>
        public CommandHandlerFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <inheritdoc />
        public TCommandHandler GetHandler<TCommandHandler>() where TCommandHandler : class, ICommandHandler
        {
            if (!typeof(TCommandHandler).IsInterface)
                throw new ArgumentException("Due to Autofac limitations you must use interfaces");

            return _lifetimeScope.Resolve<TCommandHandler>();
        }

        /// <inheritdoc />
        public ICommandHandler<TCommand> GetHandlerFor<TCommand>() where TCommand : class, ICommand
            => _lifetimeScope.Resolve<ICommandHandler<TCommand>>();

        /// <inheritdoc />
        public ICommandHandler<TCommand ,TResult> GetHandlerFor<TCommand, TResult>() where TCommand : class, ICommand<TResult>
            => _lifetimeScope.Resolve<ICommandHandler<TCommand, TResult>>();
    }
}