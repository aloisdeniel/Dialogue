using Dialogue.Portable;
using Dialogue.Portable.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue
{
    public interface IRepository<T> where T : class,IEntity
    {
        Task<ResultsPage<T>> ReadAll(int offset, int limit);

        Task<T> Read(int id);

        Task<int> Create(T entity);

        Task Update(T entity);

        Task Delete(int id);

        Task<int> Count();
    }
}
