using Dialogue.Portable;
using Dialogue.Portable.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue
{
    public class MemoryRepository<T> : IRepository<T> where T : class,IEntity
    {
        private static int lastIndex = 0;
        private static readonly Dictionary<int, T> entities = new Dictionary<int, T>();

        public MemoryRepository()
        {

        }

        private void Verify(int id)
        {
            if (!entities.ContainsKey(id))
                throw new ArgumentException("Identifier not found");
        }

        public Task<int> Create(T entity)
        {
            entity.Id = lastIndex++;
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = entity.CreatedAt;

            entities[entity.Id] = entity;

            return Task.FromResult(entity.Id);
        }

        public Task Delete(int id)
        {
            this.Verify(id);

            entities.Remove(id);

            return Task.FromResult(true);
        }

        public Task<T> Read(int id)
        {
            this.Verify(id);

            return Task.FromResult(entities[id]);
        }

        public Task<ResultsPage<T>> ReadAll(int skip, int take)
        {
            var set = entities.Values.OrderBy((a) => a.Id).Skip(skip).Take(take);
            var total = entities.Count;
            var next = skip + take;

            return Task.FromResult(new ResultsPage<T>
            {
                Items = set,
                Total = total,
                Next = (next < total) ? (int?)next : null,
            });
        }

        public Task Update(T entity)
        {
            this.Verify(entity.Id);

            entities[entity.Id] = entity;
            entity.ModifiedAt = DateTime.Now;

            return Task.FromResult(true);
        }

        public Task<int> Count()
        {
            return Task.FromResult(entities.Count);
        }
    }
}
