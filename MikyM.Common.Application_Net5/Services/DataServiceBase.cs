using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikyM.Common.Application_Net5.Interfaces;
using MikyM.Common.DataAccessLayer_Net5.UnitOfWork;
using MikyM.Common.Utilities_Net5.Results;

namespace MikyM.Common.Application_Net5.Services
{
    /// <summary>
    /// Base data service
    /// </summary>
    /// <inheritdoc cref="IDataServiceBase{TContext}"/>
    public abstract class DataServiceBase<TContext> : IDataServiceBase<TContext> where TContext : DbContext
    {
        /// <summary>
        /// <see cref="IMapper"/> instance
        /// </summary>
        protected readonly IMapper Mapper;
        /// <summary>
        /// <see cref="IUnitOfWork"/> instance
        /// </summary>
        protected readonly IUnitOfWork<TContext> UnitOfWork;
        private bool _disposed;

        /// <summary>
        /// Creates a new instance of <see cref="DataServiceBase{TContext}"/>
        /// </summary>
        /// <param name="mapper">Instance of <see cref="IMapper"/></param>
        /// <param name="uof">Instance of <see cref="IUnitOfWork{TContext}"/></param>
        protected DataServiceBase(IMapper mapper, IUnitOfWork<TContext> uof)
        {
            Mapper = mapper;
            UnitOfWork = uof;
        }

        /// <inheritdoc />
        public virtual async Task<Result<int>> CommitAsync(string? auditUserId)
        {
            return await UnitOfWork.CommitAsync(auditUserId);
        }

        /// <inheritdoc />
        public virtual async Task<Result<int>> CommitAsync()
        {
            return await UnitOfWork.CommitAsync();
        }

        /// <inheritdoc />
        public virtual async Task<Result> RollbackAsync()
        {
            await UnitOfWork.RollbackAsync();
            return Result.FromSuccess();
        }

        /// <inheritdoc />
        public virtual async Task<Result> BeginTransactionAsync()
        {
            await UnitOfWork.UseTransactionAsync();
            return Result.FromSuccess();
        }

        // Public implementation of Dispose pattern callable by consumers.
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        /// <summary>
        /// Dispose action
        /// </summary>
        /// <param name="disposing">Whether disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) UnitOfWork.Dispose();

            _disposed = true;
        }
    }
}