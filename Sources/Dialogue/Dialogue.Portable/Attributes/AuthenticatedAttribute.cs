using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Portable.Attributes
{
    public class AuthenticatedAttribute
    {
        public string[] Claims { get; set; }
    }
}
