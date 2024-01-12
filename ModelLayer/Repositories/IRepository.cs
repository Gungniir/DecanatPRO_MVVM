using System.Collections.Generic;

namespace ModelLayer.Repositories
{
    public interface IRepository<T>
    {
        T Create(T entity);

        List<T> Read();

        T Update(T entity);

        bool Delete(int id);
    }
}
