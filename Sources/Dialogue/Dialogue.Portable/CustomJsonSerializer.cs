using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dialogue.Portable
{
	public class CustomJsonSerializer : JsonSerializer
	{
		public CustomJsonSerializer()
		{
			this.ContractResolver = new CamelCasePropertyNamesContractResolver();
			this.Formatting = Formatting.Indented;

		}
	}
}

