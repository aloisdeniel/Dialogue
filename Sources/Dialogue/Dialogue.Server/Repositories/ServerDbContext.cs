using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Dialogue.Server.Repositories
{
	public class ServerDbContext : DbContext
	{

		public ServerDbContext(IEnumerable<Type> allEntities) : base("DialogueDatabase")
		{
            this.allEntities = allEntities;
		}
        
        private IEnumerable<Type> allEntities;

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			foreach (var entityType in allEntities)
			{
				var method = modelBuilder.GetType().GetMethod("Entity");
				method = method.MakeGenericMethod(new Type[] { entityType });
				method.Invoke(modelBuilder, null);
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}

