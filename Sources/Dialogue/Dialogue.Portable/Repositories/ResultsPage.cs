using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Portable.Repositories
{
    public class ResultsPage<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int Total { get; set; }

        public int? Next { get; set; }
    }
}
