using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Logics
{
    public static class ObjectSorter
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, OrderRequest orderRequest)
        {
            // Creating a parameter for the expression tree with the name x, so we can do this for example: x.name
            var param = Expression.Parameter(typeof(T), "x");

            // create an instance of the requested property (column)
            var prop = Expression.Property(param, orderRequest.FieldName);

            // create a lambda expresion, so we get something like this: x.name
            var exp = Expression.Lambda(prop, param);

            // define if you want to order by ascending or descending
            string method = orderRequest.Ascending ? "OrderBy" : "OrderByDescending";

            // create a type array with the data requeired to call the expression
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };

            // created the expression to execute, like this: object.OrderBy(x => x.name).AsNoTracking();
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);

            // execute the created expression and return its data
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}
