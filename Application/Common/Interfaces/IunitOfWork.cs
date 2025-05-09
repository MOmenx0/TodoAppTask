using AGI.Morn.Domain.Entities;
using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Application.Common.Interfaces
{
    public interface IunitOfWork :IDisposable
    {
        ITodoRepository ToDoRepository { get; } 
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RollbackTransactionAsync();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
