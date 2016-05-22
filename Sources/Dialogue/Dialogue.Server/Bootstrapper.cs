using System;
using Dialogue.Portable;
using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Dialogue.Server
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			container.Register<JsonSerializer, CustomJsonSerializer>();
		}
	}
}

