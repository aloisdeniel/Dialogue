using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Portable
{
    public class QueryParser
    {
        public static class Operators
        {
            public const string And = "{0}and{1}";
            public const string Or = "{0}or{1}";
        }
        public static class Expressions
        {
			public const string Not = "[not{0}]";
            public const string Value = "[@{0}]";
            public const string GreaterThan = "[@{0}>{1}]";
            public const string LowerThan = "[@{0}<{1}]";
            public const string Equal = "[@{0}=={1}]";
            public const string NotEqual = "[@{0}!={1}]";
            public const string Like = "[@{0}~={1}]";
        }

        public Expression<Func<T,bool>> Parse<T>(string query)
        {
			throw new NotImplementedException();
        }
    }
}
