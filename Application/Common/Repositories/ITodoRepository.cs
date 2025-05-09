using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Morn.Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Repositories
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<IEnumerable<Todo>> GetAllAsync(TodoStatus? status = null);

    }
}
