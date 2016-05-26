using System;
namespace Dialogue.Portable
{
	public abstract class EntityBase : IEntity
	{
		public EntityBase()
		{
		}

		public DateTime CreatedAt
		{
			get; set;
		}

		public int Id
		{
			get; set;
		}

		public DateTime ModifiedAt
		{
			get; set;
		}
	}
}

