using Dialogue.Server;
using Nancy.Hosting.Self;
using System;

namespace Dialogue.Sample.Server
{
	class MainClass
	{

        public class SampleServiceServer : ServiceServer
        {
            public SampleServiceServer()
            {
                this.Setup();
            }

            protected override IRepository<TEntity> CreateRepository<TEntity>()
            {
                return new MemoryRepository<TEntity>();
            }
        }

		public static void Main(string[] args)
		{
            Nancy.StaticConfiguration.DisableErrorTraces = false;

            var hostAddress = "http://localhost:8910";

            var bootstrapper = new DialogueBootstrapper<SampleServiceServer>();
            
            using (var host = new NancyHost(bootstrapper,new Uri(hostAddress)))
            {
                host.Start();
                Console.WriteLine($"Running on {hostAddress}");

                Console.ReadKey();
                host.Stop();
            }
        }
	}
}
