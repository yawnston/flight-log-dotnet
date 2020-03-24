using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("FlightLogNet.Tests.Startup", "FlightLogNet.Tests")]

namespace FlightLogNet.Tests
{
	public class Startup : DependencyInjectionTestFramework
	{
		public Startup(IMessageSink messageSink) : base(messageSink) { }

		protected void ConfigureServices(IServiceCollection services)
		{
			InjectConfiguration.Initialization(services);
			services.AddAutoMapper(typeof(AutoMapperProfile));
		}

		protected override IHostBuilder CreateHostBuilder(AssemblyName assemblyName) =>
			base.CreateHostBuilder(assemblyName)
				.ConfigureServices(ConfigureServices);
	}
}
