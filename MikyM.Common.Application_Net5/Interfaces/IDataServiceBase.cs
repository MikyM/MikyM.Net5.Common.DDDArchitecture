using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MikyM.Common.Utilities_Net5.Results;

namespace MikyM.Common.Application_Net5.Interfaces
{
    /// <summary>
    /// Base data service
    /// </summary>
    /// <typeparam name="TContext">Type that derives from <see cref="DbContext"/></typeparam>
    public interface IDataServiceBase<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// Commits pending changes
        /// </summary>
        /// <returns> <see cref="Result"/> with number of affected rows</returns>
        Task<Result<int>> CommitAsync();
        /// <summary>
        /// Commits pending changes with specifying user that is responsible for them
        /// </summary>
        /// <param name="auditUserId">Id of the user that's responsible for the changes</param>
        /// <returns> <see cref="Result"/> with number of affected rows</returns>
        Task<Result<int>> CommitAsync(string? auditUserId);
        /// <summary>
        /// Rolls the current transaction back
        /// </summary>
        /// <returns>Task with a <see cref="Result"/> representing the async operation</returns>
        Task<Result> RollbackAsync();
        /// <summary>
        /// Begins a transaction
        /// </summary>
        /// <returns>Task with a <see cref="Result"/> representing the async operation</returns>
        Task<Result> BeginTransactionAsync();
    }
}
