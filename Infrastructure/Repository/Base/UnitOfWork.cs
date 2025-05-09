using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Domain.Entities;
using AGI.Morn.Infrastructure.Data;
using Application.Common.Repositories;
using Domain.Entities;
using Infrastructure.Repository.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Infrastructure.Repository.Base
{
    public class UnitOfWork : IunitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;


        public ITodoRepository ToDoRepository {  get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ToDoRepository = new TodoRepository(_context);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

    }
}
