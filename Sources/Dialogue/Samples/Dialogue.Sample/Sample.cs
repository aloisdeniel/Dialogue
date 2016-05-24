using Dialogue.Portable;
using Dialogue.Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Sample
{
    public static class SampleExtensions
    {
        public static IService Setup(this IService service)
        {
            service.Register<Customer>();

            return service;
        }
    }
}
