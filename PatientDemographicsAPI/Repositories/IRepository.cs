using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity is created
        T Add(T entity);

        // Gets all entities of a particular type
        IEnumerable<T> GetAll();
    }
}
