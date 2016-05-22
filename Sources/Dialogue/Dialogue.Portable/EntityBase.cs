using System;
namespace Dialogue.Portable
{
	public class EntityBase : IEntity
	{
		public EntityBase()
		{
		}

		public DateTime CreatedAt
		{
			get; set;
		}

		public Guid Identifier
		{
			get; set;
		}

		public DateTime ModifiedAt
		{
			get; set;
		}
	}
}

