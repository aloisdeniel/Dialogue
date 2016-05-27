using Dialogue.Portable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Sample.Entities
{
    public class Customer : EntityBase
    {
        public string Name { get; set; }
        
    }
}
