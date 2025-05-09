using AGI.Morn.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Application.Common.Specifications
{
    public static class SpecificationsEvaluator <T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecifications<T> specifications)
        {
            var query = InputQuery;
            if (specifications.Cretiria is not null)
            {
                query = query.Where(specifications.Cretiria);
            }
            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDes is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDes);
            }

            if (specifications.IsPaginationEnabled)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            if (specifications.Includes!.Count > 0)
                query = specifications.Includes!.Aggregate(query, (CurrentQuery, IncludeExpression)
                                 => CurrentQuery.Include(IncludeExpression));
            if (specifications.IncludeStrings!.Count > 0)
                foreach (var incluse in specifications.IncludeStrings)
                    query = query.Include(incluse);

            return query;
        }

    }
}
