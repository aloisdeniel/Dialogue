using Dialogue.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Client.Repositories
{
    public class DistantRepository<T> : IRepository<T> where T : IEntity
    {
        public DistantRepository(ServiceClient client)
        {
            this.client = client;
        }

        private ServiceClient client;

        public Task<Guid> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
