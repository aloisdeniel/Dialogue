using System;
using Dialogue.Portable;
using Humanizer;
using Nancy;
using System.Collections.Generic;
using Nancy.ModelBinding;
using System.Linq;

namespace Dialogue.Server
{
	public abstract class ServiceServer : NancyModule, IService
	{
		public ServiceServer() : base()
		{
		}

        public const int DefaultLimit = 40;

        private List<Type> registeredEntities = new List<Type>();

        public IEnumerable<Type> GetRegisteredEntities()
        {
            return registeredEntities.ToList();
        }

        protected abstract IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class,IEntity;
        
		public void Register<TEntity>() where TEntity : class,IEntity
		{
            this.registeredEntities.Add(typeof(TEntity));

            var rootPath = Mapper.Current.CreateRootPath<TEntity>();
            var entityPath = Mapper.Current.CreateEntityPath<TEntity>();
            
            this.Get[rootPath, true] = async (p, ct) =>
            {
                var repository = this.CreateRepository<TEntity>();
                
                var skip = (int?)this.Request.Query.skip ?? 0;
                var take = (int?)this.Request.Query.take ?? DefaultLimit;

                var entities = await repository.ReadAll(skip,take);
                return Response.AsJson(entities);
            };

            this.Get[entityPath, true] = async (p, ct) =>
            {
                var repository = this.CreateRepository<TEntity>();

                var entity = await repository.Read((int)p.id);
                return Response.AsJson(entity);
            };

            this.Post[entityPath, true] = async (p, ct) =>
            {
                var repository = this.CreateRepository<TEntity>();

                var entity = this.Bind<TEntity>();
                if(entity != null)
                {
                    await repository.Update(entity);
                    return Response.AsJson(entity.Id);
                }

                return 404;
            };

            this.Post[rootPath, true] = async (p, ct) =>
            {
                var repository = this.CreateRepository<TEntity>();

                var entity = this.Bind<TEntity>();
                if (entity != null)
                {
                    var guid = await repository.Create(entity);
                    return Response.AsJson(guid);
                }

                return 404;
            };

            this.Delete[entityPath, true] = async (p, ct) =>
            {
                var repository = this.CreateRepository<TEntity>();
                await repository.Delete((int)p.id);
                return Response.AsJson(true);
            };
        }
	}
}

