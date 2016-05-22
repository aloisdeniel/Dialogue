using System;
using Dialogue.Portable;
using Humanizer;
using Nancy;

namespace Dialogue.Server
{
	public class ServiceServer : NancyModule, IService
	{
		public ServiceServer()
		{
			
		}

		private string findEntityName(Type entityType)
		{
			var name = entityType.Name.Camelize();
			return name;
		}

		public void Register(Type entityType)
		{
			if (!typeof(IEntity).IsAssignableFrom(entityType))
			{
				throw new ArgumentException("Registered type should inherit from IEntity");
			}

			var name = this.findEntityName(entityType);
			var rootPath = $"/{name}";
			var entityPath = $"{rootPath}/{{id}}";

			this.Get[rootPath] = (p) => "ALL";
			this.Get[entityPath] = (p) => "ONE";
			this.Post[entityPath] = (p) => "MODIFY";
			this.Post[rootPath] = (p) => "CREATE";
			this.Delete[entityPath] = (p) => "DELETE";
		}

		public void Register<TEntity>() where TEntity : IEntity
		{
			this.Register(typeof(TEntity));
		}
	}
}

