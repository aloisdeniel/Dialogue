using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Dialogue.Server
{
	public class ServerDbContext : DbContext
	{

		public ServerDbContext() : base("ServerDbContext")
		{
		}

		private List<Type> sets = new List<Type>();

		public void Register(Type entityType)
		{
			sets.Add(entityType);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			foreach (var entityType in sets)
			{
				var method = modelBuilder.GetType().GetMethod("Entity");
				method = method.MakeGenericMethod(new Type[] { entityType });
				method.Invoke(modelBuilder, null);
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}

