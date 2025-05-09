using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Application.Common.Models;
using AGI.Morn.Application.Common.Specifications;
using AGI.Morn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Infrastructure.Repository.Base
{
    public class BaseRepository <T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region ReadOne
        public T GetById(int id) => _context.Set<T>().Find(id);

        public Task<T> GetByIdAsync(Guid id) => _context.Set<T>().FindAsync(id).AsTask();

        public T GetOne(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return query.SingleOrDefault();
        }

        public TResult SelectOne<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);
            return query.AsNoTracking().Select(selector).SingleOrDefault();
        }
        public T GetFirst() => _context.Set<T>().FirstOrDefault();

        public T GetLast() => _context.Set<T>().LastOrDefault();

        public T ReadOnlyOne(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return query.AsNoTracking().SingleOrDefault();
        }
        #endregion

        #region ReadOneAsync
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public async Task<T> GetOneAsync(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<TResult> SelectOneAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);
            return await query.AsNoTracking().Select(selector).SingleOrDefaultAsync();
        }

        public async Task<T> GetFirstAsync() => await _context.Set<T>().FirstOrDefaultAsync();

        public async Task<T> GetLastAsync() => await _context.Set<T>().LastOrDefaultAsync();

        public async Task<T> ReadOnlyOneAsync(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return await query.AsNoTracking().SingleOrDefaultAsync();
        }
        #endregion

        #region ReadList
        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public IEnumerable<T> GetList(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return query.ToList();
        }

        public IEnumerable<TResult> SelectList<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);
            return query.AsNoTracking().Select(selector).ToList();
        }

        public IEnumerable<T> ReadOnlyAll() => _context.Set<T>().AsNoTracking().ToList();

        public IEnumerable<T> ReadOnlyList(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return query.AsNoTracking().ToList();
        }
        #endregion

        #region ReadListAsync
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetListAsync(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> SelectListAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);
            return await query.AsNoTracking().Select(selector).ToListAsync();
        }

        public async Task<IEnumerable<T>> ReadOnlyAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> ReadOnlyListAsync(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region MapGet
        public IQueryable<T> MapGet()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> MapGet(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return query;
        }

        public IQueryable<T> MapReadOnly()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> MapReadOnly(ISpecifications<T> specs)
        {
            IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);

            return query.AsNoTracking();
        }
        #endregion

        //#region PagedList
        //public async Task<PaginatedList<T>> PaginatedListAsync(ISpecifications<T> specs, int pageNumber, int pageSize)
        //{
        //    IQueryable<T> query = SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specs);
        //    return await query.PaginatedListAsync(pageNumber, pageSize);
        //}
        //#endregion

        #region Write
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void ExecuteUpdate(Expression<Func<T, bool>> criteria, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
        {
            _context.Set<T>().Where(criteria).ExecuteUpdate<T>(setPropertyCalls);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void ExecuteDelete(Expression<Func<T, bool>> criteria)
        {
            _context.Set<T>().Where(criteria).ExecuteDelete();
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }
        #endregion

        #region WriteAsync
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public async Task ExecuteDeleteAsync(Expression<Func<T, bool>> criteria)
        {
            await _context.Set<T>().Where(criteria).ExecuteDeleteAsync();
        }

        public async Task ExecuteUpdateAsync(Expression<Func<T, bool>> criteria, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
        {
            await _context.Set<T>().Where(criteria).ExecuteUpdateAsync(setPropertyCalls);
        }

        #endregion

        #region Operations
        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        #endregion

        #region OperationsAsync
        public async Task<int> CountAsync() => await _context.Set<T>().CountAsync();

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria) => await _context.Set<T>().CountAsync(criteria);


        #endregion

    }
}
