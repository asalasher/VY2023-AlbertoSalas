using System.Collections.Generic;

namespace DDDWorkersManager._3Domain.Contracts
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(string id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string id);
    }
}
