using System;
using System.Collections.Generic;

namespace Dialogue.Portable
{
	public interface IService
	{
        IEnumerable<Type> GetRegisteredEntities();


        void Register<TEntity>() where TEntity : class,IEntity;
	}
}

