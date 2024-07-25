using Microsoft.Extensions.Logging;

using Morph.Base.DependencyInjection;
using Morph.View.Controllers.Interfaces;

namespace Morph.View.Controllers
{
	/// <inheritdoc cref="IApplicationController" />
	[Service(typeof(IApplicationController))]
	public class ApplicationController : IApplicationController
	{
		private readonly ILogger<ApplicationController> _logger;

		public ApplicationController(ILogger<ApplicationController> logger)
		{
			_logger = logger;
		}

		public void Run()
		{
			_logger.LogInformation($"\nWelcome to Morph!\n\nTry running the tests to see Morph in action.\n");
		}
	}
}
