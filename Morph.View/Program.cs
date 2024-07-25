using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Morph.View.Controllers.Interfaces;
using Morph.Base.DependencyInjection;

namespace Morph.View
{
	internal class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">
		/// A list of arguments provided to the application.
		/// </param>
		private static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.Title = "Morpher";

			// setup our DI
			var serviceCollection = new ServiceCollection().AddLogging(cfg => cfg.AddConsole());

			// add PokerBot services from Morph.Services and Morph.View using reflection
			serviceCollection.AddServicesFromAssembly(Assembly.GetAssembly(typeof(IApplicationController)));

			// instantiate depenedency injection concrete object
			var serviceProvider = serviceCollection.BuildServiceProvider();

			// start the application by getting the Application
			// class from the required services, and run it.
			serviceProvider.GetRequiredService<IApplicationController>().Run();

			return;
		}
	}
}