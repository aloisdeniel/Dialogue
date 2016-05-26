using Dialogue.Client;
using Dialogue.Sample.Entities;
using System;
using System.Threading.Tasks;

namespace Dialogue.Sample.Client.Console
{
	class MainClass
	{
		public static void Main(string[] args)
		{

            MainAsync().Wait();
            // or, if you want to avoid exceptions being wrapped into AggregateException:
            //  MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var service = new ServiceClient("http://localhost:8910");

            service.Setup();

            var repository = service.GetRepository<Customer>();

            var r = await repository.ReadAll(0, 40);

            System.Console.WriteLine($"Total items : {r.Total}");

            var newId = await repository.Create(new Customer()
            {
                Name = "New example"
            });

            System.Console.WriteLine($"Inserted new item : {newId}");
            
            r = await repository.ReadAll(0, 40);
            
            System.Console.WriteLine($"Total items : {r.Total}");
            
            var item = await repository.Read(newId);

            System.Console.WriteLine($"Get item name : {item.Name}");

            await repository.Delete(newId);

            System.Console.WriteLine($"Deleted item : {newId}");

            r = await repository.ReadAll(0, 40);

            System.Console.WriteLine($"Total items : {r.Total}");

            System.Console.ReadKey();

        }
    }
}
