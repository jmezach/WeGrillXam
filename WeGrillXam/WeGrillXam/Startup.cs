using Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace WeGrillXam
{
	public class Startup : ShinyStartup
	{
		public override void ConfigureServices(IServiceCollection services, IPlatform platform)
		{
			services.UseBleClient();
		}
	}
}

