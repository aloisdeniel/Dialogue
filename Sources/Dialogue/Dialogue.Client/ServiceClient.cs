using Dialogue.Portable;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dialogue.Client
{
	public class ServiceClient : IService
	{

		public ServiceClient()
		{
            this.Client = new HttpClient();
		}

        public HttpClient Client { get; set; }

        public void Register<TEntity>() where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

    }
}

