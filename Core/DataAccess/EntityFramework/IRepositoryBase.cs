using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public interface IRepositoryBase<T> where T : class, IEntity, new()
    {
        List<T> GetList(Func<T, bool> filter = null);
        T Get(Func<T, bool> filter);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
