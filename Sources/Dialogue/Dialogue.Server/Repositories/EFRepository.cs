using Dialogue.Portable;
using Dialogue.Portable.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Server.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class,IEntity
    {
        private readonly IEnumerable<Type> allEntities;

        public EFRepository(IEnumerable<Type> allEntities)
        {
            this.allEntities = allEntities;
        }

        public Task<int> Count()
        {
            using (var context = new ServerDbContext(this.allEntities))
            {
                return context.Set<TEntity>().CountAsync();
            }
        }

        public async Task<int> Create(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = entity.CreatedAt;

            using (var context = new ServerDbContext(this.allEntities))
            {
                context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                await context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public async Task Delete(int id)
        {
            using (var context = new ServerDbContext(this.allEntities))
            {
                var entity = await context.Set<TEntity>().Where((e) => e.Id == id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<TEntity> Read(int id)
        {
            using (var context = new ServerDbContext(this.allEntities))
            {
                var entity = context.Set<TEntity>().Where((e) => e.Id == id);
                return await entity.FirstOrDefaultAsync();
            }
        }

        public async Task<ResultsPage<TEntity>> ReadAll(int skip, int take)
        {
            using (var context = new ServerDbContext(this.allEntities))
            {
                var set = context.Set<TEntity>();
                var total = await set.CountAsync();
                var next = skip + take;
                var entitites = await set.OrderBy((a) => a.Id).Skip(skip).Take(take).ToListAsync();
                return new ResultsPage<TEntity>
                {
                    Items = entitites as IEnumerable<TEntity>,
                    Total = total,
                    Next = (next < total) ? (int?)next : null
                };
            }
        }

        public async Task Update(TEntity entity)
        {
            using (var context = new ServerDbContext(this.allEntities))
            {
                entity.ModifiedAt = DateTime.Now;
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
