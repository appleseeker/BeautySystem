using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Extensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> IQueryable, int pageIndex, int pageSize)
        {
            PagedList<T> list = new PagedList<T>(IQueryable, pageIndex, pageSize);
            return list;
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> IEnumerable, int pageIndex, int pageSize,int TotleCount)
        {
            PagedList<T> list = new PagedList<T>(IEnumerable, pageIndex, pageSize, TotleCount);
            return list;
        }
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> IEnumerable,  int TotleCount)
        {
            PagedList<T> list = new PagedList<T>(IEnumerable, TotleCount);
            return list;
        }
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> IEnumerable, int pageIndex, int pageSize)
        {
            PagedList<T> list = new PagedList<T>(IEnumerable, pageIndex, pageSize, IEnumerable.Count());
            return list;
        }

        public static IQueryable<T> OrderByQueryable<T>(this IQueryable<T> source, string propertyName, bool ascending) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException("propertyName", "Not Exist");

            ParameterExpression param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
