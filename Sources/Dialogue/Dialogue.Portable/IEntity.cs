using System;
namespace Dialogue.Portable
{
	public interface IEntity
	{
		Guid Identifier { get; set; }

		DateTime CreatedAt { get; set; }

		DateTime ModifiedAt { get; set; }
	}
}

