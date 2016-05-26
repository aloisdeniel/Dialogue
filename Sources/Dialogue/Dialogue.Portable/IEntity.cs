using System;
namespace Dialogue.Portable
{
	public interface IEntity
	{
		int Id { get; set; }

		DateTime CreatedAt { get; set; }

		DateTime ModifiedAt { get; set; }
	}
}

