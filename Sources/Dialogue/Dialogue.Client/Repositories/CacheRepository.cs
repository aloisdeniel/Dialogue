using System;
using System.Threading.Tasks;
using Dialogue.Portable.Repositories;

namespace Dialogue.Client
{
	public class CacheRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
	{
		public CacheRepository()
		{
		}

		public Task<int> Count()
		{
			throw new NotImplementedException();
		}

		public Task<int> Create(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity> Read(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResultsPage<TEntity>> ReadAll(int offset, int limit)
		{
			throw new NotImplementedException();
		}

		public Task Update(TEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}

