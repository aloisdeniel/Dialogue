using System;
using Dialogue.Portable;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Dialogue.Server
{
	public class DialogueBootstrapper<TService> : DefaultNancyBootstrapper where TService : ServiceServer
    {
        public DialogueBootstrapper()
        {
        }
        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            this.RegisterModules(container, new ModuleRegistration[] { new ModuleRegistration(typeof(TService)) });

            base.ApplicationStartup(container, pipelines);
        }


        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			container.Register<JsonSerializer, CustomJsonSerializer>();
		}
	}
}

