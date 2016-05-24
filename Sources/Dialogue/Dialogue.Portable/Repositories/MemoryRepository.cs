using Dialogue.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue
{
    public class MemoryRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly Dictionary<Guid, T> entities = new Dictionary<Guid, T>();

        public MemoryRepository()
        {

        }

        private void Verify(Guid id)
        {
            if (id == null || !this.entities.ContainsKey(id))
                throw new ArgumentException("Identifier not found");
        }

        public Task<Guid> Create(T entity)
        {
            var guid = Guid.NewGuid();

            entity.Identifier = guid;
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = entity.CreatedAt;

            entities[guid] = entity;

            return Task.FromResult(guid);
        }

        public Task Delete(Guid id)
        {
            this.Verify(id);

            entities.Remove(id);

            return Task.FromResult(true);
        }

        public Task<T> Read(Guid id)
        {
            this.Verify(id);

            return Task.FromResult(entities[id]);
        }

        public Task<IEnumerable<T>> ReadAll()
        {
            return Task.FromResult<IEnumerable<T>>(entities.Values);
        }

        public Task Update(T entity)
        {
            this.Verify(entity.Identifier);

            entities[entity.Identifier] = entity;
            entity.ModifiedAt = DateTime.Now;

            return Task.FromResult(true);
        }
    }
}
