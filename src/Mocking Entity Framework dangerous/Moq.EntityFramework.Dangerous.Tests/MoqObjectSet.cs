using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace Moq.EntityFramework.Dangerous.Tests
{
    public class MoqDbSet<T> : IDbSet<T> where T : class
    {
        readonly HashSet<T> _data;
        readonly IQueryable _query;

        public MoqDbSet()
            : this(Enumerable.Empty<T>())
        {
        }

        public MoqDbSet(IEnumerable<T> entities)
        {
            _data = new HashSet<T>();
            foreach (var entity in entities)
            {
                _data.Add(entity);
            }
            _query = _data.AsQueryable();
        }

        public void AddObject(T entity)
        {
            Local.Add(entity);
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from MoqDbSet and override Find");
        }

        public T Add(T entity)
        {
            Local.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            Local.Remove(entity);
            return entity;
        }

        T IDbSet<T>.Attach(T entity)
        {
            Local.Add(entity);
            return entity;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(_data); }
        }

       
        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _query.ElementType; }
        }

        public Expression Expression
        {
            get { return _query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _query.Provider; }
        }

        
    }
}
