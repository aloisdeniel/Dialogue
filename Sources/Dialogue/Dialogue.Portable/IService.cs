using System;
namespace Dialogue.Portable
{
	public interface IService
	{
		void Register<TEntity>() where TEntity : IEntity;

		void Register(Type entityType);
	}
}

