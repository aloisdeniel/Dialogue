using System;
using Dialogue.Portable;
using Humanizer;
using Nancy;
using System.Collections.Generic;
using Nancy.ModelBinding;

namespace Dialogue.Server
{
	public abstract class ServiceServer : NancyModule, IService
	{
		public ServiceServer()
		{
		}

        private static Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        private IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity
        {
            var t = typeof(TEntity);

            if (repositories.ContainsKey(t))
            {
                return repositories[t] as IRepository<TEntity>;
            }

            var repo = this.CreateRepository<TEntity>();

            repositories[t] = repo;

            return repo;
        }

        protected abstract IRepository<TEntity> CreateRepository<TEntity>() where TEntity : IEntity;
        
		public void Register<TEntity>() where TEntity : IEntity
		{
            var rootPath = Mapper.Current.CreateRootPath<TEntity>();
            var entityPath = Mapper.Current.CreateEntityPath<TEntity>();

            var repository = this.GetRepository<TEntity>();

            this.Get[rootPath, true] = async (p, ct) => 
            {
                var entities = await repository.ReadAll();
                return Response.AsJson(entities);
            };

            this.Get[entityPath, true] = async (p, ct) => 
            {
                var entity = await repository.Read((Guid)p.id);
                return Response.AsJson(entity);
            };

            this.Post[entityPath, true] = async (p, ct) =>
            {
                var entity = this.Bind<TEntity>();
                await repository.Update(entity);
                return Response.AsJson(entity.Identifier);
            };

            this.Post[rootPath, true] = async (p, ct) =>
            {
                var entity = this.Bind<TEntity>();
                var guid = await repository.Create(entity);
                return Response.AsJson(guid);
            };

            this.Delete[entityPath, true] = async (p, ct) =>
            {
                await repository.Delete((Guid)p.id);
                return Response.AsJson(true);
            };
        }
	}
}

