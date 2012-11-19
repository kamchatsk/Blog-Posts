using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace Moq.EntityFramework
{
    public static class Extensions
    {
        public static IQueryable<TSource> Include<TSource>(this IQueryable<TSource> source, string path)
        {
            var objectQuery = source as ObjectQuery<TSource>;

            if (objectQuery != null)
            {
                return objectQuery.Include(path);
            }

            return source;
        }
    }
}
