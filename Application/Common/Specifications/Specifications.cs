using AGI.Morn.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Application.Common.Specifications
{
    public class Specifications <T> :ISpecifications<T> where T : class
    {
        public Specifications() {

        }
        public Specifications(Expression<Func<T, bool>> expression)
        {
            Cretiria = expression;
        }
        public Expression<Func<T, bool>>? Cretiria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get;  set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDes { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public List<string> IncludeStrings => new List<string>();

        public void AddOrderby(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;

        }
        public void AddOrderbyDesc(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDes = OrderByDescExpression;

        }
        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes?.Add(includeExpression);
        }
    }
}
