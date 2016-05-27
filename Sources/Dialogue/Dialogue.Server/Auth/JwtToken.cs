using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Server.Auth
{
    public class JwtToken
    {

        public string Sub { get; set; }

        public long Exp { get; set; }
    }
}
