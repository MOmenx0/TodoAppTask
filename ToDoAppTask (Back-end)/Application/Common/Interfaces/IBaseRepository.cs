using AGI.Morn.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace AGI.Morn.Application.Common.Interfaces
{
        public interface IBaseRepository<T> where T : class
        {
            #region ReadOne
            T GetById(int id);
            T GetOne(ISpecifications<T> specs);
            TResult SelectOne<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs);
            T ReadOnlyOne(ISpecifications<T> specs);
            T GetFirst();
            T GetLast();
        #endregion

        #region ReadOneAsync
            Task<T> GetByIdAsync(Guid id);
            Task<T> GetByIdAsync(int id);
            Task<T> GetOneAsync(ISpecifications<T> specs);
            Task<TResult> SelectOneAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs);
            Task<T> ReadOnlyOneAsync(ISpecifications<T> specs);
            Task<T> GetFirstAsync();
            Task<T> GetLastAsync();
            #endregion

            #region ReadList
            IEnumerable<T> GetAll();
            IEnumerable<T> GetList(ISpecifications<T> specs);
            IEnumerable<TResult> SelectList<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs);
            IEnumerable<T> ReadOnlyAll();
            IEnumerable<T> ReadOnlyList(ISpecifications<T> specs);
            #endregion

            #region ReadListAsync
            Task<IEnumerable<T>> GetAllAsync();
            Task<IEnumerable<T>> GetListAsync(ISpecifications<T> specs);
            Task<IEnumerable<TResult>> SelectListAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecifications<T> specs);
            Task<IEnumerable<T>> ReadOnlyAllAsync();
            Task<IEnumerable<T>> ReadOnlyListAsync(ISpecifications<T> specs);
            #endregion

            #region MapGet
            IQueryable<T> MapGet();
            IQueryable<T> MapGet(ISpecifications<T> specs);
            IQueryable<T> MapReadOnly();
            IQueryable<T> MapReadOnly(ISpecifications<T> specs);
            #endregion

            //#region PagedList
            //Task<PaginatedList<T>> PaginatedListAsync(ISpecifications<T> specs, int pageNumber, int pageSize);
            //#endregion

            #region Write
            T Add(T entity);
            void AddRange(IEnumerable<T> entities);
            T Update(T entity);
            void UpdateRange(IEnumerable<T> entities);
            void Delete(T entity);
            void DeleteRange(IEnumerable<T> entities);
            void Attach(T entity);
            void AttachRange(IEnumerable<T> entities);
            void ExecuteUpdate(Expression<Func<T, bool>> criteria, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
            void ExecuteDelete(Expression<Func<T, bool>> criteria);
            #endregion

            #region WriteAsync
            Task<T> AddAsync(T entity);
            Task AddRangeAsync(IEnumerable<T> entities);
            Task ExecuteUpdateAsync(Expression<Func<T, bool>> criteria, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
            Task ExecuteDeleteAsync(Expression<Func<T, bool>> criteria);
            #endregion

            #region Operations
            int Count();
            int Count(Expression<Func<T, bool>> criteria);
            #endregion

            #region OperationsAsync
            Task<int> CountAsync();
            Task<int> CountAsync(Expression<Func<T, bool>> criteria);
            #endregion
        }
    }

