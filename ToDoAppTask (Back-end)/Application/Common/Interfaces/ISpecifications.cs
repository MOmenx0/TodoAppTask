using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Application.Common.Interfaces
{
    public interface ISpecifications<T> where T : class
    
    {
        Expression<Func<T, bool>>? Cretiria { get; set; }
        List<Expression<Func<T, object>>>? Includes { get; set; }
        List<string>? IncludeStrings { get; }
        Expression<Func<T, object>>? OrderBy { get; set; }
        Expression<Func<T, object>>? OrderByDes { get; set; }
        int Take { get; set; }
        int Skip { get; set; }
        bool IsPaginationEnabled { get; set; }
    }
}
