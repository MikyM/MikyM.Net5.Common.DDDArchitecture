namespace MikyM.Common.Application_Net5.CommandHandlers.Commands
{
    /// <summary>
    /// Base command marker interface, used only internally
    /// </summary>
    public interface IBaseCommand
    {
    }

    /// <summary>
    /// Represents a base command
    /// </summary>
    public interface ICommand : IBaseCommand
    {
    }

    /// <summary>
    /// Represents a base command with a result
    /// </summary>
    public interface ICommand<TResult> : IBaseCommand
    {
    }
}
