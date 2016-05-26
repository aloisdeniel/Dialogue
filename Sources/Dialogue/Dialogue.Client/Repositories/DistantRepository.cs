using Dialogue.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialogue.Portable.Repositories;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace Dialogue.Client.Repositories
{
    public class DistantRepository<TEntity> : IRepository<TEntity> where TEntity : class,IEntity
    {
        public struct Void { }

        public DistantRepository(ServiceClient client)
        {
            this.client = client;
			this.serializer = new CustomJsonSerializer();
			this.CurrentRequests = new List<Request>();
        }

        private ServiceClient client;

		private readonly CustomJsonSerializer serializer;

		#region Serialization

		private T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
			{
				ContractResolver = this.serializer.ContractResolver,
				Formatting = this.serializer.Formatting,
			});
		}

		private string Serialize<T>(T json)
		{
			return JsonConvert.SerializeObject(json, new JsonSerializerSettings()
			{
				ContractResolver = this.serializer.ContractResolver,
				Formatting = this.serializer.Formatting,
			});
		}

		#endregion

		#region Sending requests

		private class Request
		{
			public string Url { get; set; }

			public DateTime StartDate { get; set; }

			public HttpMethod Method { get; set; }

			public HttpContent Body { get; set; }

			public object Task {get; set;}
		}

		private List<Request> CurrentRequests { get; set; }

		private Request GetCurrentRequest(string url, HttpMethod method, HttpContent body)
		{
			return this.CurrentRequests.FirstOrDefault((r) => r.Url == url && r.Method == method);
		}

		private Task<T> SendMergedAsync<T>(string url, HttpMethod method, HttpContent body)
		{
			var currentRequest = this.GetCurrentRequest(url, method, body);
				
			if (currentRequest != null)
			{
				return (Task<T>)currentRequest.Task;
			}

			var task = SendAsync<T>(url,method,body);

			currentRequest = new Request()
			{
				Url = url,
				Method = method,
				StartDate = DateTime.Now,
				Task = task,
			};

			this.CurrentRequests.Add(currentRequest);
			task.ContinueWith((t) => { this.CurrentRequests.Remove(currentRequest); });

			return task;
		}

		private async Task<T> SendAsync<T>(string url, HttpMethod method, HttpContent body)
		{
			var message = new HttpRequestMessage(method, url);

			if (body != null)
			{
				message.Content = body;
                message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

			var resp = await this.client.Client.SendAsync(message);

            if(typeof(T) != typeof(Void))
            {
                var content = await resp.Content.ReadAsStringAsync();
                return this.Deserialize<T>(content);
            }

            return default(T);

		}

		private Task<T> SendAsync<T,TBody>(string url, HttpMethod method, TBody body)
		{
			var jsonBody = this.Serialize<TBody>(body);
			var content = new StringContent(jsonBody);
			return this.SendAsync<T>(url, method, content);
		}

		#endregion

        public Task<int> Create(TEntity entity)
        {
			var rootPath = Mapper.Current.CreateRootPath<TEntity>();

            rootPath = this.client.Host + rootPath;

			return this.SendAsync<int, TEntity>(rootPath, HttpMethod.Post, entity);
        }

        public Task Delete(int id)
        {
			var entityPath = Mapper.Current.CreateEntityPath<TEntity>(id);
            entityPath = this.client.Host + entityPath;
            return this.SendAsync<Void>(entityPath, HttpMethod.Delete, null);
        }

        public Task<TEntity> Read(int id)
        {
			var entityPath = Mapper.Current.CreateEntityPath<TEntity>(id);
            entityPath = this.client.Host + entityPath;
            return this.SendMergedAsync<TEntity>(entityPath, HttpMethod.Get, null);
        }

        public Task Update(TEntity entity)
        {
			var entityPath = Mapper.Current.CreateEntityPath<TEntity>(entity.Id);
            entityPath = this.client.Host + entityPath;
            return this.SendAsync<Void,TEntity>(entityPath, HttpMethod.Post, entity);
        }

        public Task<ResultsPage<TEntity>> ReadAll(int offset, int limit)
        {
			var rootPath = Mapper.Current.CreateRootPath<TEntity>();

            rootPath = this.client.Host + rootPath;
            return this.SendMergedAsync<ResultsPage<TEntity>>(rootPath, HttpMethod.Get, null);
        }

        public async Task<int> Count()
        {
			var all = await this.ReadAll(0, 0);
			return all.Total;
        }
    }
}
