using System;
namespace Dialogue.Portable
{
	public interface IService
	{
		void Register<TEntity>() where TEntity : IEntity;
	}
}

