using Dialogue.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialogue.Portable.Repositories;

namespace Dialogue.Client.Repositories
{
    public class DistantRepository<T> : IRepository<T> where T : class,IEntity
    {
        public DistantRepository(ServiceClient client)
        {
            this.client = client;
        }

        private ServiceClient client;

        public Task<int> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Read(int id)
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

        public Task<ResultsPage<T>> ReadAll(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }
    }
}
