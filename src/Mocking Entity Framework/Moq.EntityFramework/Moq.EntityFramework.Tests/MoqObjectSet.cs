using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Moq.EntityFramework.Tests
{
    public class MoqObjectSet<T> : IObjectSet<T> where T : class
    {
        public MoqObjectSet()
            : this(Enumerable.Empty<T>())
        {
        }

        public MoqObjectSet(IEnumerable<T> entities)
        {
            _set = new HashSet<T>();
            foreach (var entity in entities)
            {
                _set.Add(entity);
            }
            _queryableSet = _set.AsQueryable();
        }

        public void AddObject(T entity)
        {
            _set.Add(entity);
        }

        public void Attach(T entity)
        {
            _set.Add(entity);
        }

        public void DeleteObject(T entity)
        {
            _set.Remove(entity);
        }

        public void Detach(T entity)
        {
            _set.Remove(entity);
        }

        public Type ElementType
        {
            get { return _queryableSet.ElementType; }
        }

        public Expression Expression
        {
            get { return _queryableSet.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _queryableSet.Provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        readonly HashSet<T> _set;
        readonly IQueryable<T> _queryableSet;
    }
}
