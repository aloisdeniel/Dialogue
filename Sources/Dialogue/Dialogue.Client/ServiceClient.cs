using Dialogue.Portable;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dialogue.Client.Repositories;
using System.Linq;

namespace Dialogue.Client
{
	public class ServiceClient : IService
	{
		public ServiceClient()
		{
            this.Client = new HttpClient();
			this.MergeRequests = true;
		}

        public HttpClient Client { get; set; }

		public bool MergeRequests { get; set; }

		private Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public void Register<TEntity>() where TEntity : class,IEntity
        {
			this.repositories[typeof(TEntity)] = new DistantRepository<TEntity>(this);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class,IEntity
        {
			return this.repositories[typeof(TEntity)] as IRepository<TEntity>;
        }
        public IEnumerable<Type> GetRegisteredEntities()
        {
			return this.repositories.Keys.ToList();
        }
    }
}

