using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Morn.Infrastructure.Data;
using AGI.Morn.Infrastructure.Repository.Base;
using Application.Common.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Repositories
{
    internal class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Todo>> GetAllAsync(TodoStatus? status = null)
        {
            var query = _context.Todos.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            return await query.ToListAsync();
        }
    }
}
