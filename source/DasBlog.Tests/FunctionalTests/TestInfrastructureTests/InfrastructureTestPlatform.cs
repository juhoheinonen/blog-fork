using DasBlog.Tests.FunctionalTests.Common;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace DasBlog.Tests.FunctionalTests.TestInfrastructureTests
{
	public class InfrastructureTestPlatform : TestSupportPlatform
	{
		protected override void InjectDependencies(IServiceCollection services)
		{
			// nothing to do
		}

		protected override void CompleteSetupLocal()
		{
			// nothing to do
		}
	}
}
