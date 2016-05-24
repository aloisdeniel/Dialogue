using Dialogue.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Server
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> ReadAll();

        Task<T> Read(Guid id);

        Task<Guid> Create(T entity);

        Task Update(T entity);

        Task Delete(Guid id);
    }
}
