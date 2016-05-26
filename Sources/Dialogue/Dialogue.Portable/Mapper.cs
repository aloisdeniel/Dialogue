using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Portable
{
    public class Mapper
    {
        #region Instance
        private static Mapper current;
        private static object syncRoot = new Object();

        public static Mapper Current
        {
            get
            {
                if (current == null)
                {
                    lock (syncRoot)
                    {
                        if (current == null)
                            current = new Mapper();
                    }
                }

                return current;
            }
            set
            {
                lock (syncRoot)
                {
                    current = value;
                }
            }
        }

        #endregion

        public Mapper()
        {

        }

        private string BasePath { get; set; } = "/api";

        protected virtual string FindEntityName<TEntity>()
        {
            var name = typeof(TEntity).Name.Pluralize().Camelize();
            return name;
        }

        public virtual string CreateRootPath<TEntity>()
        {
            var name = this.FindEntityName<TEntity>();
            return $"{BasePath}/{name}";
        }

        public virtual string CreateEntityPath<TEntity>()
        {
            var rootPath = this.CreateRootPath<TEntity>();
            return $"{rootPath}/{{id:int}}";
        }
    }
}
